using Data.ValueObject;
using Keys;
using UnityEngine;
namespace Commands
{
    public class JoyStickMoveCommand 
    {
        #region Self Variables

        #region Private Variables

        private CharacterController _characterController;
        private PlayerMovementData _playerMovementData;
        private Rigidbody _rigidbody;

        #endregion

        #endregion

        public JoyStickMoveCommand(ref Rigidbody rigidbody,ref PlayerMovementData playerMovementData,ref CharacterController characterController)
        {

            _playerMovementData = playerMovementData;
            _rigidbody = rigidbody;
            _characterController = characterController;
        }

        public void Execute(InputParams _inputParams)
        {
            Vector3 _movement = new Vector3(_inputParams.Values.x * _playerMovementData.PlayerJoystickSpeed, 0,
                _inputParams.Values.z * _playerMovementData.PlayerJoystickSpeed);
            // _characterController.Move(_movement * Time.fixedDeltaTime);
            _rigidbody.velocity=_movement;
            if (_movement != Vector3.zero)
            {
                Quaternion _newDirect = Quaternion.LookRotation(_movement);
                _rigidbody.transform.rotation = _newDirect;
            }
            

        
        }
    }
}