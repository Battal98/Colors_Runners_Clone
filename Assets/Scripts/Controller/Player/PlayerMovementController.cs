using Commands;
using Commands.Player;
using Data.ValueObject;
using Keys;
using Managers;
using UnityEngine;
using Enums;
using Sirenix.OdinInspector;


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

        [ShowInInspector] [Header("Data")] private PlayerMovementData _playerMovementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private int _inDroneArea = 1;
        private Vector3 _inputValue;
        private Vector2 _clampValues;
        private GameStates _states;
        private InputParams _inputParams;
        private MoveSwerveCommand _moveSwerveCommand;
        private StopSideWaysCommand _stopSideWaysCommand;
        private JoyStickMoveCommand _joyStickMoveCommand;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _moveSwerveCommand = new MoveSwerveCommand(ref rigidbody, ref _playerMovementData);
            _stopSideWaysCommand = new StopSideWaysCommand(ref rigidbody, ref _playerMovementData);
            _joyStickMoveCommand = new JoyStickMoveCommand(ref rigidbody, ref _playerMovementData);
        }

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

        public void InDroneArea(int state)
        {
            _inDroneArea = state;
        }


        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                ChangeMoveType();
            }
            else
                Stop();
        }

        private void ChangeMoveType()
        {
            switch (_states)
            {
                case GameStates.Runner:
                    MoveSwerve();
                    break;
                case GameStates.Idle:
                    Moveidle();
                    break;
            }
        }

        private void Moveidle()
        {
            if (_isReadyToMove)
            {
                _joyStickMoveCommand.Execute(_inputParams);
            }
            else
            {
                rigidbody.velocity = Vector3.zero;
            }
        }

        private void MoveSwerve()
        {
            if (_isReadyToMove)
            {
                _moveSwerveCommand.Execute(_inputParams);
            }
            else
            {
                _stopSideWaysCommand.Execute();
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