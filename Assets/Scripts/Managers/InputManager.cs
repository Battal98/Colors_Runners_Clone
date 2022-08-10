using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Keys;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region SelfVariables

        #region Public Variables
        private bool _isTouching;//ref type
        private float _currentVelocity;//ref type
        private Vector3? _mousePosition;//ref type
        private Vector3 _joystickPos;//ref type
        private Vector3 _moveVector;//ref type



        #endregion
        #region Serialized Variables

        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;
        [SerializeField] bool isJoystick = false;
        [SerializeField] private Joystick joystick;


        #endregion
        #region Private Variables

        private InputData _inputData;


        #endregion

        #endregion

        private void Awake()
        {
            _inputData = GetInputData();
            Debug.Log(_inputData);
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;

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
            CoreGameSignals.Instance.onGetGameState += OnSetGameStates;

        }
        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onGetGameState -= OnSetGameStates;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

       

        #endregion
        private void Update()
        {
            if (!isReadyForTouch) return;

            if (Input.GetMouseButtonUp(0))
            {
                EndOfDragging();
            }


            if (Input.GetMouseButtonDown(0))
            {

                StartOfDragging();
            }

            if (Input.GetMouseButton(0))
            {
                if (_isTouching)
                {
                    if (isJoystick)
                    {
                        DuringOnDraggingJoystick();
                    }
                    else
                    {
                        if (_mousePosition != null)
                        {
                            DuringOnDragging();
                        }
                    }

                }
            }
        }

        private void EndOfDragging()
        {
            _isTouching = false;
            InputSignals.Instance.onInputReleased?.Invoke();
            _joystickPos = Vector3.zero;
            _moveVector = _joystickPos;
            InputSignals.Instance.onInputDragged?.Invoke(new InputParams()
            {
                Values = _moveVector,
            });
        }
        private void StartOfDragging()
        {
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!isFirstTimeTouchTaken)
            {
                isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }

            _mousePosition = Input.mousePosition;
            _joystickPos = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        }
        private void DuringOnDragging()
        {
            Vector3 mouseDeltaPos = (Vector3)Input.mousePosition - _mousePosition.Value;
            if (mouseDeltaPos.x > _inputData.PlayerInputSpeed)
                _moveVector.x = _inputData.PlayerInputSpeed / 10f * mouseDeltaPos.x;
            else if (mouseDeltaPos.x < -_inputData.PlayerInputSpeed)
                _moveVector.x = -_inputData.PlayerInputSpeed / 10f * -mouseDeltaPos.x;
            else
                _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                    _inputData.ClampSpeed);

            _mousePosition = Input.mousePosition;

            InputSignals.Instance.onInputDragged?.Invoke(new InputParams()
            {
                Values = _moveVector,
                ClampValues = new Vector2(_inputData.ClampSides.x, _inputData.ClampSides.y)
            });
        }

        private void DuringOnDraggingJoystick()
        {
            _joystickPos = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            _moveVector = _joystickPos;

            InputSignals.Instance.onInputDragged?.Invoke(new InputParams()
            {
                Values = _moveVector,
            });
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

        private void OnSetGameStates(GameStates states)
        {
            if (states==GameStates.Idle)
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