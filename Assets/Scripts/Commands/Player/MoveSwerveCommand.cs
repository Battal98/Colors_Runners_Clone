using Data.ValueObject;
using Keys;
using UnityEngine;

namespace Commands.Player
{
    public class MoveSwerveCommand
    {
        #region Self Variables

        #region Private Variables

        private Rigidbody _rigidbody;
        // private InputParams _inputParams;
        private PlayerMovementData _playerMovementData;
        #endregion
        #endregion

        public MoveSwerveCommand(ref Rigidbody rigidbody,
            ref PlayerMovementData playerMovementData)
        {
            _rigidbody = rigidbody;
    
            _playerMovementData = playerMovementData;
        }

        public void Execute(InputParams _inputParams)
        {
            
            _rigidbody.velocity = new Vector3(
                _inputParams.Values.x * _playerMovementData.SidewaysSpeed,
                _rigidbody.velocity.y,
                _playerMovementData.ForwardSpeed);
           


            _rigidbody.position = new Vector3(
                // Mathf.Clamp(_rigidbody.position.x, -_inputParams.ClampValues.x, _inputParams.ClampValues.x),
                _rigidbody.position.x,
                _rigidbody.position.y ,
                _rigidbody.position.z );
        }
    }
}