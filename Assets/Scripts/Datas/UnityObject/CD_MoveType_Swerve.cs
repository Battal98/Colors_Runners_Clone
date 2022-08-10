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
       

        public override void DoMovement(ref bool _isReadyToMove,ref Rigidbody _rigidbody, ref Vector3 _inputValue, ref PlayerMovementData _moveData, ref Vector2 _clampValues, ref CharacterController characterController, Transform transform)
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
        private void SwerveMove(ref Rigidbody rigidbody, ref PlayerMovementData movementData, ref Vector3 inputValue, ref Vector2 clampValues)
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(inputValue.x * movementData.SidewaysSpeed, velocity.y,
                movementData.ForwardSpeed ) ;
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, clampValues.x,
                    clampValues.y),
                (position = rigidbody.position).y,
                position.z);
            rigidbody.position = position;
            RotCalculater(ref inputValue, rigidbody,velocity);


        }

        private void StopSideways(Rigidbody rigidbody, ref PlayerMovementData movementData)
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, movementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.transform.DOLocalRotate(Vector3.zero, 0.1f);
        }

            #endregion
        private void RotCalculater(ref Vector3 inputValue, Rigidbody rigidbody, Vector3 position)
        {
            if (inputValue.x == 0)
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