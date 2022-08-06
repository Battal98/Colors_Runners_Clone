
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
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void Unsubscribe()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
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
            Debug.Log(Data);
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;
        private void SendPlayerDataToControllers()
        {
            _playerMovementController.SetMovementData(Data.MovementData);   
        }
        
        private void OnActivateMovement()
        {
            _playerMovementController.EnableMovement();
            
        }

        private void OnDeactiveMovement()
        {
            _playerMovementController.DeactiveMovement();
        }
        private void OnGetInputValues(InputParams inputParams)
        {
            _playerMovementController.UpdateInputValue(inputParams);
           
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