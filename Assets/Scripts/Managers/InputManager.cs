using Commands;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public Vector3? MousePosition; //ref type

        #endregion

        #region Serialized Variables

        [SerializeField] private bool isJoystick;
        [SerializeField] private Joystick joystick;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;

        #endregion

        #region Private Variables

        private bool _isTouching; 
        private float _currentVelocity;
        private Vector3 _joystickPos; 
        private Vector3 _moveVector; 
        private InputData _inputData;
        private EndOfDraggingCommand _endOfDraggingCommand;
        private StartOfDraggingCommand _startOfDraggingCommand;
        private DuringOnDraggingCommand _duringOnDraggingCommand;
        private DuringOnDraggingJoystickCommand _duringOnDraggingJoystickCommand;

        #endregion

        #endregion

        private void Awake()
        {
            _inputData = GetInputData();
            Init();
        }

        private void Init()
        {
            _endOfDraggingCommand = new EndOfDraggingCommand(ref _joystickPos, ref _moveVector);
            _startOfDraggingCommand = new StartOfDraggingCommand(ref _joystickPos, ref isFirstTimeTouchTaken,
                ref joystick, ref inputManager);
            _duringOnDraggingCommand = new DuringOnDraggingCommand(ref _inputData, ref _moveVector,
                ref _currentVelocity, ref inputManager);
            _duringOnDraggingJoystickCommand =
                new DuringOnDraggingJoystickCommand(ref _joystickPos, ref _moveVector, ref joystick);
        }

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").InputData;
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGetGameState += OnGetGameStates;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onGetGameState -= OnGetGameStates;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        #endregion

        private void Update()
        {
            if (!isReadyForTouch) return;
            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
                _endOfDraggingCommand.Execute();
            }

            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;
                _startOfDraggingCommand.Execute();
            }

            if (Input.GetMouseButton(0))
                if (_isTouching)
                {
                    if (isJoystick)
                    {
                        _duringOnDraggingJoystickCommand.Execute();
                    }
                    else
                    {
                        if (MousePosition != null) _duringOnDraggingCommand.Execute();
                    }
                }
        }

        #region SubscribedMethods

        private void OnGetGameStates(GameStates states)
        {
            if (states == GameStates.Idle)
            {
                joystick.gameObject.SetActive(true);
                isJoystick = true;
            }
            else
            {
                joystick.gameObject.SetActive(false);
                isJoystick = false;
            }
        }

        private void OnPlay()
        {
            isReadyForTouch = true;
        }

        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }

        #endregion
    }
}