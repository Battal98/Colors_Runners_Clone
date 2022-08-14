using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Commands
{
    public class DuringOnDraggingCommand : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables
        private Vector3? _mousePosition;
        private InputData _inputData;
        private Vector3 _moveVector;
        private float _currentVelocity;
        #endregion
        #endregion
        
        public DuringOnDraggingCommand(ref Vector3 mousePosition,ref InputData inputData,ref Vector3 moveVector,ref float currentVelocity)
        {
            _mousePosition = mousePosition;
            _inputData = inputData;
            _moveVector = moveVector;
            _currentVelocity = currentVelocity;
        }
        
        public void Execute()
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
    }
}