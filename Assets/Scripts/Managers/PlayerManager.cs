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

        #endregion

        #region Private Variables

        #endregion

        #endregion

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
            CoreGameSignals.Instance.onExitColorCheckArea += OnExitColorCheckArea;
        }

        private void Unsubscribe()
        {
            InputSignals.Instance.onInputTaken -= playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased -= playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged -= playerMovementController.UpdateInputValue;
            CoreGameSignals.Instance.onGetGameState -= OnGetGameState;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onExitColorCheckArea -= OnExitColorCheckArea;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        private void SendPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(Data.MovementData);
        }

        private void OnGetGameState(GameStates states)
        {
            playerAnimationController.gameObject.SetActive(true);
            playerMovementController.ChangeStates(states);
        }

        private void OnSetScoreText(int Values)
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

        private void OnPlay()
        {
            playerMovementController.IsReadyToPlay(true);
        }
        private void OnReset()
        {
            gameObject.SetActive(true);
            playerMovementController.OnReset();
            playerAnimationController.OnReset();
        }
    }
}