using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    public abstract class Movement : ScriptableObject
    {
        public abstract void DoSwerveMovement(ref bool _isReadyToMove,Rigidbody _rigidbody,ref Vector2 _inputValue,ref PlayerMovementData _moveData,ref Vector2 _clampValue);

        public abstract void DoJoystickMovement(ref bool _isReadyToMove, Rigidbody _rigidbody, ref Vector3 _inputValue, ref PlayerMovementData _moveData, ref CharacterController characterController, Transform transform);

    }
}