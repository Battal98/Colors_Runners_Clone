using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class StartOfDraggingCommand : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables
        
        
        private bool _isTouching;
        private Vector3 _joystickPos;
        private bool _isFirstTimeTouchTaken;
        private Vector3? _mousePosition;
        private Joystick _joystick;
        #endregion
        #endregion

        public StartOfDraggingCommand(ref bool isTouching,ref Vector3 joystickPos,ref bool isFirstTimeTouchTaken,ref Vector3 mousePosition, ref Joystick joystick)
        {
            
            _isTouching = isTouching;
            _joystickPos = joystickPos;
            _isFirstTimeTouchTaken = isFirstTimeTouchTaken;
            _mousePosition = mousePosition;
            _joystick = joystick;
        }
        public void Execute()
        {
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }

            _mousePosition = Input.mousePosition;
            _joystickPos = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);

        }
    }
}