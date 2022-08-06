using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    public abstract class Movement : ScriptableObject
    {
        public abstract void DoMovement(ref bool _isReadyToMove,Rigidbody _rigidbody,ref Vector2 _inputValue,ref PlayerMovementData _moveData,ref Vector2 _clampValue);
    }
}