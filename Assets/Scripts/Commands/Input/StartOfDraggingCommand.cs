using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class StartOfDraggingCommand
    {
        #region Self Variables

        #region Private Variables

        private Vector3 _joystickPos;
        private bool _isFirstTimeTouchTaken;
        private readonly InputManager _manager;
        private readonly Joystick _joystick;

        #endregion

        #endregion

        public StartOfDraggingCommand(ref Vector3 joystickPos, ref bool isFirstTimeTouchTaken,
            ref Joystick joystick, ref InputManager manager)
        {
            _joystickPos = joystickPos;
            _isFirstTimeTouchTaken = isFirstTimeTouchTaken;
            _joystick = joystick;
            _manager = manager;
        }

        public void Execute()
        {
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }

            _manager.MousePosition = Input.mousePosition;
            _joystickPos = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        }
    }
}