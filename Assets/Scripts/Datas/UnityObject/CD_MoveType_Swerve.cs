using Data.ValueObject;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine.UI;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Swerve", menuName = "Movement/SwerveMove", order = 0)]
    public class CD_MoveType_Swerve : CD_Movement
    {
        public override void DoMovement(ref int inDroneArea, ref bool _isReadyToMove, ref Rigidbody _rigidbody,
            ref Vector3 _inputValue, ref PlayerMovementData _moveData, ref Vector2 _clampValues,
            ref CharacterController characterController, Transform transform)
        {
            if (_isReadyToMove)
            {
                SwerveMove(ref inDroneArea, ref _rigidbody, ref _moveData, ref _inputValue, ref _clampValues);
            }
            else
            {
                StopSideways(ref inDroneArea, ref _rigidbody, ref _moveData);
            }
        }

        private void SwerveMove(ref int _inDroneArea, ref Rigidbody rigidbody, ref PlayerMovementData movementData,
            ref Vector3 inputValue, ref Vector2 clampValues)
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(inputValue.x * movementData.SidewaysSpeed, velocity.y,
                movementData.ForwardSpeed * _inDroneArea);
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, clampValues.x,
                    clampValues.y),
                (position = rigidbody.position).y,
                position.z);
            rigidbody.position = position;
            // RotCalculater(ref inputValue, rigidbody, velocity);
        }

        private void StopSideways(ref int _inDroneArea, ref Rigidbody rigidbody, ref PlayerMovementData movementData)
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, movementData.ForwardSpeed * _inDroneArea);
            rigidbody.angularVelocity = Vector3.zero;

            rigidbody.transform.rotation = Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.identity, 1f);
            // rigidbody.transform.DOLocalRotate(Vector3.zero, 0.1f);
        }


        private void RotCalculater(ref Vector3 inputValue, Rigidbody rigidbody, Vector3 position)
        {
            if (inputValue.x == 0)
            {
                rigidbody.transform.rotation =
                    Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.identity, 1);

                // rigidbody.transform.DOLocalRotate(Vector3.zero, 0.05f);
            }
            else
            {
                rigidbody.transform.rotation = Quaternion.Slerp(
                    quaternion.identity,
                    new Quaternion(
                       0,  rigidbody.velocity.x*Time.fixedDeltaTime,0, 1
                    ),
                    1
                );


                // rigidbody.transform.rotation = Quaternion.Lerp(rigidbody.transform.rotation, new Quaternion(rigidbody.transform.rotation.x,rigidbody.velocity.x,rigidbody.transform.rotation.z,1), 0.95f);
                // rigidbody.transform.DOLocalRotate(new Vector3(0, position.x * 10f, 0), 0.05f);
            }
        }
    }
}