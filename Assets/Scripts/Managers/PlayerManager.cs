
using System.Collections;
using UnityEngine;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Player.Controllers;
using TMPro;
using Signals;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        #endregion

        #region Serialized Variables

        [SerializeField]private PlayerAnimationController _playerAnimationController;
        [SerializeField]private PlayerMovementController _playerMovementController;
        [SerializeField]private PlayerPhysicsController _playerPhysicsController;
        [SerializeField]private Rigidbody rb;
        [SerializeField]private CapsuleCollider col;
        [SerializeField]private CharacterController characterController;

        #endregion
        #region Public Variables

        [Header("Data")] public PlayerData Data;
        #endregion

        #region Serialized Variables

        [SerializeField] private TextMeshPro scoreText;
        #endregion

        #endregion


        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            InputSignals.Instance.onInputTaken += _playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased +=  _playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged += _playerMovementController.UpdateInputValue;
            CoreGameSignals.Instance.onGetGameState += OnGetCameraState;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void Unsubscribe()
        {
            InputSignals.Instance.onInputTaken -= _playerMovementController.EnableMovement;
            InputSignals.Instance.onInputReleased -=  _playerMovementController.DeactiveMovement;
            InputSignals.Instance.onInputDragged -= _playerMovementController.UpdateInputValue;
            CoreGameSignals.Instance.onGetGameState -= OnGetCameraState;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;


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
            _playerMovementController.SetMovementData(Data.MovementData);   
        }

        private void OnGetCameraState(GameStates states)
        {

            ChechGameStates(states);
            _playerMovementController.ChangeStates(states);
        }

        private void ChechGameStates(GameStates states)
        {
            if (states==GameStates.Runner)
            {
                col.enabled = true;
                rb.isKinematic = false;
                rb.useGravity = true;
                characterController.enabled = false;

            }
            else if (states==GameStates.Idle)
            {
                col.enabled = false;
                rb.isKinematic = true;
                rb.useGravity = false;
                characterController.enabled = true;

            }
        }

        private void OnLevelSuccessful()
        {
            _playerMovementController.IsReadyToPlay(false);

        }
        private void OnLevelFailed()
        {
            _playerMovementController.IsReadyToPlay(false);
        }

        public void SetStackPosition()
        {
            Vector2 pos = new Vector2(transform.position.x,transform.position.z);
            // StackSignals.Instance.onStackFollowPlayer?.Invoke(pos);
        }

      
        private void OnSetScoreText(int Values)
        {
            scoreText.text = Values.ToString();
        }

        IEnumerator WaitForFinal()
        {
            _playerAnimationController.Playanim(animationStates:PlayerAnimationStates.Idle);
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
            // CoreGameSignals.Instance.onMiniGameStart?.Invoke();
        }

     
        private void OnPlay()
        {
            _playerMovementController.IsReadyToPlay(true);
            _playerAnimationController.Playanim(PlayerAnimationStates.Run);
        }
        private void OnReset()
        {
            gameObject.SetActive(true);
            _playerMovementController.OnReset();
            _playerAnimationController.OnReset();
        }

        
       
        
    }
}