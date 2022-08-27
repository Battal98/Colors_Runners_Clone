using System.Collections.Generic;
using Commands;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;

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

        private bool _isTouching; //ref type
        private float _currentVelocity; //ref type
        private Vector3 _joystickPos; //ref type
        private Vector3 _moveVector; //ref type
        private InputData _inputData;
        private EndOfDraggingCommand _endofDraggingCommand;
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
            _endofDraggingCommand = new EndOfDraggingCommand(ref _joystickPos, ref _moveVector);
            _startOfDraggingCommand = new StartOfDraggingCommand(ref _joystickPos, ref isFirstTimeTouchTaken,
                ref joystick, ref inputManager);
            _duringOnDraggingCommand =
                new DuringOnDraggingCommand(ref _inputData, ref _moveVector, ref _currentVelocity, ref inputManager);
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
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGetGameState += OnGetGameStates;
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
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
                _endofDraggingCommand.Execute();
            }


            if (Input.GetMouseButtonDown(0) )
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


        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }


        #region SubscribedMethods

        private void OnEnableInput()
        {
            isReadyForTouch = true;
        }

        private void OnDisableInput()
        {
            isReadyForTouch = false;
        }

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