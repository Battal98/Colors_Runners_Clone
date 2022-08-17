using System.Collections;
using UnityEngine;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using TMPro;
using Signals;

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
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private CapsuleCollider capsuleCollider;
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
            CoreGameSignals.Instance.onPlayerChangeForwardSpeed += playerMovementController.OnPlayerChangeForwardSpeed;
            CoreGameSignals.Instance.onExitDroneArea += OnExitDroneArea;
        }

        private void Unsubscribe()
        {
            InputSignals.Instance.onInputTaken -= playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased -= playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged -= playerMovementController.UpdateInputValue;
            CoreGameSignals.Instance.onGetGameState -= OnGetGameState;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlayerChangeForwardSpeed -= playerMovementController.OnPlayerChangeForwardSpeed;
            CoreGameSignals.Instance.onExitDroneArea -= OnExitDroneArea;
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

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

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


        private void OnPlay()
        {
            playerMovementController.IsReadyToPlay(true);
            playerAnimationController.Playanim(PlayerAnimationStates.Run);
        }

        private void OnReset()
        {
            gameObject.SetActive(true);
            playerMovementController.OnReset();
            playerAnimationController.OnReset();
        }

        private void OnExitDroneArea()
        {
            CameraSignals.Instance.onSetCameraTarget?.Invoke(transform);
            playerPhysicsController.ExitDroneArea();
            playerMovementController.ExitDroneArea();
        }
    }
}