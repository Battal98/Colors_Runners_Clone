using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Keys;
using Managers;
using UnityEngine;
using Enums;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private CharacterController characterController;

        #endregion

        #region Private Variables

        [Header("Data")] private PlayerMovementData _movementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private Vector3 _inputValue;
        private Vector2 _clampValues;
        private GameStates _states;
        private CD_MovementList _movementList;

        #endregion

        #endregion

        private void Awake()
        {
            _movementList = GetMovementTypeList();
        }
        private CD_MovementList GetMovementTypeList() => Resources.Load<CD_MovementList>("Data/CD_MovementList");

        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            _movementData = dataMovementData;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }

        public void ChangeStates(GameStates states)
        {

            _states = states;

        }
        public void UpdateInputValue(InputParams inputParam)
        {
            _inputValue = inputParam.Values;
            _clampValues = inputParam.ClampValues;
        }

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
               
                    _movementList.MovementTypeList[(int)_states].DoMovement(ref _isReadyToMove,ref rigidbody, ref _inputValue,
                        ref _movementData, ref _clampValues, ref  characterController, this.gameObject.transform);
            }
            else
                Stop();
        }

        // public void PlayerJump(float distance, float duration)
        // {
        //     
        // }
       
        public void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}