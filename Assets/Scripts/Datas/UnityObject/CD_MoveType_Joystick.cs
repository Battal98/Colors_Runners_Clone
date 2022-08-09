using Data.ValueObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Joystick", menuName = "Movement/JoystickMove", order = 0)]
    public class CD_MoveType_Joystick : Movement
    {
      

        public override void DoMovement(ref bool _isReadyToMove, Rigidbody _rigidbody, ref Vector3 _inputValue, ref PlayerMovementData _moveData, ref Vector2 _clampValue, ref CharacterController characterController, Transform transform)
        {
            JoystickMove(ref _rigidbody, ref _moveData, ref _inputValue, ref characterController, transform);
        }


        #region Joystick Jobs
        private void JoystickMove(ref Rigidbody rigidbody, ref PlayerMovementData _movementData, ref Vector3 _inputValue, ref CharacterController characterController, Transform transform)
        {
            Vector3 _movement = new Vector3(_inputValue.x * _movementData.PlayerJoystickSpeed, 0,
                _inputValue.z * _movementData.PlayerJoystickSpeed);
            characterController.Move(_movement * Time.fixedDeltaTime);

            #region Rotation

            if (_movement != Vector3.zero)
            {
                Quaternion _newDirect = Quaternion.LookRotation(_movement);
                transform.transform.rotation = _newDirect;
            } 
            #endregion

            #region Gravity

            var _vSpeed = 0f;
            _vSpeed -= 9.81f * Time.fixedDeltaTime * 10;
            _movement.y = _vSpeed;
            characterController.Move(_movement * Time.fixedDeltaTime); 

            #endregion


        }

        #endregion
    }
}
