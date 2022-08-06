using Data.ValueObject;
using Keys;
using Managers;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Swerve", menuName = "Movement/SwerveMove", order = 0)]
    public class CD_MoveType_Swerve : Movement
    {
     
        

        public override void DoMovement(ref bool _isReadyToMove,Rigidbody _rigidbody,ref Vector2 _inputValue,ref PlayerMovementData _moveData,ref Vector2 _clampValues)
        {
           
                if (_isReadyToMove)
                {
                    Move(ref _rigidbody,ref _moveData,ref _inputValue,ref _clampValues);
                }
                else
                {
                  StopSideways(_rigidbody,ref _moveData);
                }
            
        
        }

        private void Move(ref Rigidbody rigidbody,ref PlayerMovementData _movementData,ref Vector2 _inputValue,ref Vector2 _clampValues)
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
        }

        private void StopSideways(Rigidbody rigidbody,ref PlayerMovementData _movementData)
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _movementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }

      
    }
}