using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    public abstract class CD_Movement : ScriptableObject
    {
        public abstract void DoMovement(ref int inDroneArea,ref bool _isReadyToMove,ref Rigidbody _rigidbody,ref Vector3 _inputValue,ref PlayerMovementData _moveData,ref Vector2 _clampValue,ref CharacterController characterController, Transform transform);

        
    }
}