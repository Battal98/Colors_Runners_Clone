using Commands;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Keys;
using UnityEngine;
using Enums;
using Managers;
using Sirenix.OdinInspector;


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
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        [ShowInInspector] [Header("Data")] private PlayerMovementData _playerMovementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private float _colorAreaSpeed = 1;
        private Vector3 _inputValue;
        private Vector2 _clampValues;
        private GameStates _states;
        private InputParams _inputParams;
        // private MoveSwerveCommand _moveSwerveCommand;
        // private StopSideWaysCommand _stopSideWaysCommand;
        // private JoyStickMoveCommand _joyStickMoveCommand;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            // _moveSwerveCommand = new MoveSwerveCommand(ref rigidbody, ref _playerMovementData,ref _colorAreaSpeed);
            // _stopSideWaysCommand = new StopSideWaysCommand(ref rigidbody, ref _playerMovementData);
            // _joyStickMoveCommand = new JoyStickMoveCommand(ref rigidbody, ref _playerMovementData);
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
                    transform.DOLocalMoveZ(transform.localPosition.z + 2.9f, .5f);
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
                    ref rigidbody,
                    ref _inputParams, ref _playerMovementData);
                // ChangeMoveType();
            }
            else
                Stop();

        }

        #region PlayerMovementCommands

        // private void ChangeMoveType()
        // {
        //     switch (_states)
        //     {
        //         case GameStates.Runner:
        //             MoveSwerve();
        //             break;
        //         case GameStates.Idle:
        //             Moveidle();
        //             break;
        //     }
        // }
        //
        // private void Moveidle()
        // {
        //     if (_isReadyToMove)
        //     {
        //         _joyStickMoveCommand.Execute(_inputParams,_colorAreaSpeed);
        //     }
        //     else
        //     {
        //         rigidbody.velocity = Vector3.zero;
        //     }
        // }
        //
        // private void MoveSwerve()
        // {
        //     if (_isReadyToMove)
        //     {
        //         _moveSwerveCommand.Execute(_inputParams,_colorAreaSpeed);
        //     }
        //     else
        //     {
        //         _stopSideWaysCommand.Execute();
        //     }
        // }

        #endregion


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