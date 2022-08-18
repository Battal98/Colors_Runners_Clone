using Data.ValueObject;
using UnityEngine;

namespace Commands
{
    public class StopSideWaysCommand
    {
        #region Self Variables

        #region Private Variables

        private Rigidbody _rigidbody;
        private PlayerMovementData _playerMovementData;

        #endregion

        #endregion

        public StopSideWaysCommand(ref Rigidbody rigidbody, ref PlayerMovementData playerMovementData)
        {
            _rigidbody = rigidbody;
            _playerMovementData = playerMovementData;
        }

        public void Execute(float _colorAreaSpeed)
        {
            _rigidbody.velocity =
                new Vector3(0, _rigidbody.velocity.y, _playerMovementData.ForwardSpeed * _colorAreaSpeed);
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}