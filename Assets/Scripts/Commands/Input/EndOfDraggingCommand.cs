using Keys;
using Managers;
using UnityEngine;
using Signals;

namespace Commands
{
    public class EndOfDraggingCommand 
    {
        #region Self Variables

        #region Private Variables

        
        private bool _isTouching;
        private Vector3 _joystickPos;
        private Vector3 _moveVector;
        #endregion
        #endregion

        public EndOfDraggingCommand(ref bool isTouching, ref Vector3 joystickPos,ref Vector3 moveVector)
        {
           
            _isTouching = isTouching;
            _joystickPos = joystickPos;
            _moveVector = moveVector;
        }
        
        public void Execute()
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
    }
}