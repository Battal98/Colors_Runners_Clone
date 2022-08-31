using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private CD_MovementList cdMovementList;

        #endregion

        #region Private Variables

        [ShowInInspector] [Header("Data")] private PlayerMovementData _playerMovementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private float _colorAreaSpeed = 1;
        private Vector3 _inputValue;
        private Vector2 _clampValues;
        private GameStates _states;
        private InputParams _inputParams;

        #endregion

        #endregion

        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            _playerMovementData = dataMovementData;
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
            _inputParams = inputParam;
        }

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }

        public void PlayerChangeForwardSpeed(ColorCheckAreaType value)
        {
            Stop();
            switch (value)
            {
                case ColorCheckAreaType.Drone:
                    _colorAreaSpeed = 0;
                    break;
                case ColorCheckAreaType.Turret:
                    _colorAreaSpeed = 0.5f;
                    break;
            }
        }

        public void ExitColorCheckArea(ColorCheckAreaType areaType)
        {
            switch (areaType)
            {
                case ColorCheckAreaType.Drone:
                    _colorAreaSpeed = 1;
                    break;
                case ColorCheckAreaType.Turret:
                    _colorAreaSpeed = 1;
                    break;
            }
        }

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                cdMovementList.MovementTypeList[(int)_states].DoMovement(ref _colorAreaSpeed, ref _isReadyToMove,
                    ref rigidbody, ref _inputParams, ref _playerMovementData);
            }
            else
            {
                Stop();
            }
        }

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