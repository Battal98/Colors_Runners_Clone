using Signals;
using Keys;
using UnityEngine;

namespace Commands
{
    public class EndOfDraggingCommand
    {
        #region Self Variables

        #region Private Variables

        private Vector3 _joystickPos;
        private Vector3 _moveVector;

        #endregion

        #endregion

        public EndOfDraggingCommand(ref Vector3 joystickPos, ref Vector3 moveVector)
        {
            _joystickPos = joystickPos;
            _moveVector = moveVector;
        }

        public void Execute()
        {
            InputSignals.Instance.onInputReleased?.Invoke();
            _joystickPos = Vector3.zero;
            _moveVector = Vector3.zero;
            InputSignals.Instance.onInputDragged?.Invoke(new InputParams()
            {
                Values = _moveVector,
            });
        }
    }
}