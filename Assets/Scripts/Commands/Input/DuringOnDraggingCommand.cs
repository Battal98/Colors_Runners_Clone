using System.Collections.Generic;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using Keys;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Commands
{
    public class DuringOnDraggingCommand 
    {
        #region Self Variables

        #region Private Variables
       
        private InputData _inputData;
        private Vector3 _moveVector;
        private float _currentVelocity;
        private InputManager _manager;
        #endregion
        #endregion
        
        public DuringOnDraggingCommand(ref InputData inputData,ref Vector3 moveVector,ref float currentVelocity,ref InputManager manager )
        {
      
            _inputData = inputData;
            _moveVector = moveVector;
            _currentVelocity = currentVelocity;
            _manager = manager;
        }
        
        public void Execute()
        {
            Vector3 mouseDeltaPos = (Vector3)Input.mousePosition - _manager._mousePosition.Value;
            if (mouseDeltaPos.x > _inputData.PlayerInputSpeed)
                _moveVector.x = _inputData.PlayerInputSpeed / 10f * mouseDeltaPos.x;
            else if (mouseDeltaPos.x < -_inputData.PlayerInputSpeed)
                _moveVector.x = -_inputData.PlayerInputSpeed / 10f * -mouseDeltaPos.x;
            else
                _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f,ref _currentVelocity,
                    _inputData.ClampSpeed);

            _manager._mousePosition = Input.mousePosition;

            InputSignals.Instance.onInputDragged?.Invoke(new InputParams()
            {
                Values = _moveVector,
                ClampValues = new Vector2(_inputData.ClampSides.x, _inputData.ClampSides.y)
            });
        }
    }
}