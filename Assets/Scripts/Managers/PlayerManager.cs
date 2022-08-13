using System.Collections;
using UnityEngine;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
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

        [SerializeField]private PlayerAnimationController playerAnimationController;
        [SerializeField]private PlayerMovementController playerMovementController;
        [SerializeField]private PlayerPhysicsController playerPhysicsController;
        [SerializeField]private Rigidbody rigidbody;
        [SerializeField]private CapsuleCollider capsuleCollider;
        [SerializeField]private CharacterController characterController;
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
            InputSignals.Instance.onInputReleased +=  playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged += playerMovementController.UpdateInputValue;
            CoreGameSignals.Instance.onGetGameState += OnGetCameraState;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            StackSignals.Instance.onStackJumpPlatform += OnStackJumpPlatform;
        }

        private void Unsubscribe()
        {
            InputSignals.Instance.onInputTaken -= playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased -=  playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged -= playerMovementController.UpdateInputValue;
            CoreGameSignals.Instance.onGetGameState -= OnGetCameraState;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            StackSignals.Instance.onStackJumpPlatform -= OnStackJumpPlatform;
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

        private void OnGetCameraState(GameStates states)
        {
            ChechGameStates(states);
            playerAnimationController.gameObject.SetActive(true);
            playerMovementController.ChangeStates(states);
        }

        private void ChechGameStates(GameStates states)
        {
            if (states==GameStates.Runner)
            {
                capsuleCollider.enabled = true;
                rigidbody.isKinematic = false;
                rigidbody.useGravity = true;
                characterController.enabled = false;
            }
            else if (states==GameStates.Idle)
            {
                capsuleCollider.enabled = false;
                rigidbody.isKinematic = true;
                rigidbody.useGravity = false;
                characterController.enabled = true;
            }
        }
        private void OnStackJumpPlatform()
        {
            playerMovementController.PlayerJump(Data.MovementData.PlayerJumpDistance, Data.MovementData.PlayerJumpDistance);
        }

        private void OnLevelSuccessful()
        {
            playerMovementController.IsReadyToPlay(false);

        }
        private void OnLevelFailed()
        {
            playerMovementController.IsReadyToPlay(false);
        }
        
        
        private void OnSetScoreText(int Values)
        {
            scoreText.text = Values.ToString();
        }

        IEnumerator WaitForFinal()
        {
            playerAnimationController.Playanim(animationStates:PlayerAnimationStates.Idle);
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
            // CoreGameSignals.Instance.onMiniGameStart?.Invoke();
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

        public void PlayerStopForwards()
        {
            playerMovementController.Stop();
            playerMovementController.InDroneArea(0);
        
        }
    }
}