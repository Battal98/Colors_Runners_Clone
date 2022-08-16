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
            rigidbody.velocity = new Vector3(inputValue.x * movementData.SidewaysSpeed, rigidbody.velocity.y,
                movementData.ForwardSpeed * _inDroneArea);


            rigidbody.position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, clampValues.x,
                    clampValues.y),
                (rigidbody.position).y,
                rigidbody.position.z);
        }

        private void StopSideways(ref int _inDroneArea, ref Rigidbody rigidbody, ref PlayerMovementData movementData)
        {
         
        }
    }
}