using Data.ValueObject;
using Keys;
using Managers;
using UnityEngine;
using DG.Tweening;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Swerve", menuName = "Movement/SwerveMove", order = 0)]
    public class CD_MoveType_Swerve : Movement
    {
        public override void DoJoystickMovement(ref bool _isReadyToMove, Rigidbody _rigidbody, ref Vector3 _inputValue, ref PlayerMovementData _moveData, ref CharacterController characterController, Transform transform)
        {

        }

        public override void DoSwerveMovement(ref bool _isReadyToMove, Rigidbody _rigidbody, ref Vector2 _inputValue, ref PlayerMovementData _moveData, ref Vector2 _clampValues)
        {

            if (_isReadyToMove)
            {
                SwerveMove(ref _rigidbody, ref _moveData, ref _inputValue, ref _clampValues);
            }
            else
            {
                StopSideways(_rigidbody, ref _moveData);
            }
        }
        #region Swerve Jobs
        private void SwerveMove(ref Rigidbody rigidbody, ref PlayerMovementData _movementData, ref Vector2 _inputValue, ref Vector2 _clampValues)
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue.x * _movementData.SidewaysSpeed, velocity.y,
                _movementData.ForwardSpeed);
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                    _clampValues.y),
                (position = rigidbody.position).y,
                position.z);
            rigidbody.position = position;
            RotCalculater(ref _inputValue, rigidbody,velocity);


        }

        private void StopSideways(Rigidbody rigidbody, ref PlayerMovementData _movementData)
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _movementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.transform.DOLocalRotate(Vector3.zero, 0.1f);
        }

            #endregion
        private void RotCalculater(ref Vector2 _inputValue, Rigidbody rigidbody, Vector3 position)
        {
            if (_inputValue.x == 0)
            {
                rigidbody.transform.DOLocalRotate(Vector3.zero, 0.05f);
            }
            else
            {
                rigidbody.transform.DOLocalRotate(new Vector3(0, position.x * 10f, 0), 0.05f);
            }
        }


        
    }
}