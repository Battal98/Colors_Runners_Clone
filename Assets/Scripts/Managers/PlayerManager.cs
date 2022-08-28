using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
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

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private GameObject scoreArea;

        #endregion

        #region Private Variables

        private Transform _camera;
        private bool _scoreAreaVisible = true;

        #endregion

        #endregion

        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToControllers();
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
            InputSignals.Instance.onInputDragged += playerMovementController.UpdateInputValue;
            CoreGameSignals.Instance.onGetGameState += OnGetGameState;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccesful;
            CoreGameSignals.Instance.onExitColorCheckArea += OnExitColorCheckArea;
            ScoreSignals.Instance.onSetScore += OnSetScore;
        }

        private void Unsubscribe()
        {
            InputSignals.Instance.onInputTaken -= playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased -= playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged -= playerMovementController.UpdateInputValue;
            CoreGameSignals.Instance.onGetGameState -= OnGetGameState;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccesful;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onExitColorCheckArea -= OnExitColorCheckArea;
            ScoreSignals.Instance.onSetScore -= OnSetScore;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion


        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        // private void Update()
        // {
        //     scoreArea.transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        // }

        private void SendPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(Data.MovementData);
        }

        private void OnGetGameState(GameStates states)
        {
            playerAnimationController.gameObject.SetActive(true);
            playerMovementController.ChangeStates(states);
        }

        private void OnSetScore(int Values)
        {
            scoreText.text = Values.ToString();
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

        public void PlayAnim(PlayerAnimationStates animationStates)
        {
            playerAnimationController.PlayAnim(animationStates);
        }

        private void Start()
        {
            _camera = Camera.main.transform;
        }

        public void ChangeScoreAreaVisible(ColorCheckAreaType areaType)
        {
            if (areaType == ColorCheckAreaType.Drone)
            {
                scoreArea.SetActive(!_scoreAreaVisible);
                _scoreAreaVisible = !_scoreAreaVisible;
            }
        }

        public void DownCost()
        {
            if (int.Parse(scoreText.text) > 0)
            {
                IdleGameSignals.Instance.onCostDown?.Invoke();
            }
        }

        private void OnLevelFailed()
        {
            playerMovementController.IsReadyToPlay(false);
        }

        private void OnLevelSuccesful()
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
            playerAnimationController.OnReset();
        }
    }
}