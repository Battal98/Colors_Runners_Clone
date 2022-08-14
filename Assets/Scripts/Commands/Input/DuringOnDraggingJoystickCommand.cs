using Keys;
using Signals;
using UnityEngine;

namespace Commands
{
    public class DuringOnDraggingJoystickCommand
    {
        #region Self Variables

        #region Private Variables

        private Vector3 _joystickPos;
        private Vector3 _moveVector;
        private Joystick _joystick;

        #endregion

        #endregion

        public DuringOnDraggingJoystickCommand(ref Vector3 joystickPos, ref Vector3 moveVector, ref Joystick joystick)
        {
            _joystickPos = joystickPos;
            _moveVector = moveVector;
            _joystick = joystick;
        }

        public void Execute()
        {
            _joystickPos = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            _moveVector = _joystickPos;

            InputSignals.Instance.onInputDragged?.Invoke(new InputParams
            {
                Values = _moveVector
            });
        }
    }
}