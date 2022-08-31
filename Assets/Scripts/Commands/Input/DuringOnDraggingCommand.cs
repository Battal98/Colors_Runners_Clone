using Data.ValueObject;
using Keys;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class DuringOnDraggingCommand
    {
        #region Self Variables

        #region Private Variables

        private readonly InputData _inputData;
        private Vector3 _moveVector;
        private float _currentVelocity;
        private readonly InputManager _manager;

        #endregion

        #endregion

        public DuringOnDraggingCommand(ref InputData inputData, ref Vector3 moveVector, ref float currentVelocity,
            ref InputManager manager)
        {
            _inputData = inputData;
            _moveVector = moveVector;
            _currentVelocity = currentVelocity;
            _manager = manager;
        }

        public void Execute()
        {
            var mouseDeltaPos = Input.mousePosition - _manager.MousePosition.Value;
            if (mouseDeltaPos.x > _inputData.PlayerInputSpeed)
                _moveVector.x = _inputData.PlayerInputSpeed / 10f * mouseDeltaPos.x;
            else if (mouseDeltaPos.x < -_inputData.PlayerInputSpeed)
                _moveVector.x = -_inputData.PlayerInputSpeed / 10f * -mouseDeltaPos.x;
            else
                _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                    _inputData.ClampSpeed);

            _manager.MousePosition = Input.mousePosition;

            InputSignals.Instance.onInputDragged?.Invoke(new InputParams
            {
                Values = _moveVector,
                ClampValues = new Vector2(_inputData.ClampSides.x, _inputData.ClampSides.y)
            });
        }
    }
}