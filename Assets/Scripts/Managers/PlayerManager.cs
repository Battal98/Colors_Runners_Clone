using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using RootMotion.FinalIK;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;
        public FullBodyBipedIK FullIK;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private GameObject scoreArea;
        [SerializeField] private ParticleSystem colorParticle;

        #endregion

        #region Private Variables

        private bool _scoreAreaVisible = true;
        private GameStates _states;
        private int _score;
        private PlayerAnimationStates _animationState;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            SendPlayerDataToControllers();
        }

        private void GetReferences()
        {
            Data = GetPlayerData();
        }

        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            InputSignals.Instance.onInputTaken += playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased += playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onGetGameState += OnGetGameState;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onExitColorCheckArea += OnExitColorCheckArea;
            ScoreSignals.Instance.onSetPlayerScore += OnSetScore;

            StackSignals.Instance.onScaleSet += OnScaleSet;

            IdleGameSignals.Instance.onStageChanged += OnStageChanged;
        }


        private void Unsubscribe()
        {
            InputSignals.Instance.onInputTaken -= playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased -= playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            CoreGameSignals.Instance.onGetGameState -= OnGetGameState;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onExitColorCheckArea -= OnExitColorCheckArea;
            ScoreSignals.Instance.onSetPlayerScore -= OnSetScore;

            StackSignals.Instance.onScaleSet -= OnScaleSet;

            IdleGameSignals.Instance.onStageChanged -= OnStageChanged;
        }


        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void OnScaleSet(float value)
        {
            var localScale = transform.localScale;
            localScale = new Vector3(
                Mathf.Clamp((localScale.x + value), 0.8f, 2f),
                Mathf.Clamp((localScale.y + value), 0.8f, 2f),
                Mathf.Clamp((localScale.z + value), 0.8f, 2f)
            );
            transform.localScale = localScale;
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        private void Start()
        {
            _states = GameStates.Runner;
        }

        private void SendPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(Data.MovementData);
        }

        private void OnGetGameState(GameStates states)
        {
            _states = states;
            playerAnimationController.gameObject.SetActive(true);
            playerMovementController.ChangeStates(states);
        }

        private void OnSetScore(int values)
        {
            _score = values;
            SetScoreText(values);
        }

        private void SetScoreText(int values)
        {
            scoreText.text = values.ToString();
        }

        private void OnExitColorCheckArea(ColorCheckAreaType colorAreaType)
        {
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(transform);
            ExitColorCheckArea(colorAreaType);
        }

        public void ExitColorCheckArea(ColorCheckAreaType colorAreaType)
        {
            playerMovementController.ExitColorCheckArea(colorAreaType);
        }

        public void ChangeSpeed(ColorCheckAreaType colorAreaType)
        {
            playerMovementController.PlayerChangeForwardSpeed(colorAreaType);
        }

        private void OnInputDragged(InputParams InputParam)
        {
            playerMovementController.UpdateInputValue(InputParam);
            PlayAnim(Mathf.Abs(InputParam.Values.x + InputParam.Values.y));
        }

        private void PlayAnim(float value)
        {
            if (_states != GameStates.Idle) return;
            playerAnimationController.PlayAnim(value);
        }


        public void ChangeScoreAreaVisible(ColorCheckAreaType areaType)
        {
            if (areaType != ColorCheckAreaType.Drone) return;
            scoreArea.SetActive(!_scoreAreaVisible);
            _scoreAreaVisible = !_scoreAreaVisible;
        }

        public void DownCost()
        {
            if (_score > 0)
            {
                FullIK.enabled = true;
                IdleGameSignals.Instance.onCostDown?.Invoke();
                OnScaleSet(-0.10f);
                _score--;
                ScoreSignals.Instance.onSetIdleScore?.Invoke(_score);
                colorParticle.Play();
            }

            SetScoreText(_score);
        }

        public void OnStageChanged()
        {
            colorParticle.Stop();
            FullIK.enabled = false;
        }


        private void OnLevelFailed()
        {
            playerMovementController.IsReadyToPlay(false);
        }


        private void OnPlay()
        {
            scoreArea.SetActive(true);
            playerMovementController.IsReadyToPlay(true);
        }

        private void OnReset()
        {
            playerMovementController.OnReset();
        }
    }
}