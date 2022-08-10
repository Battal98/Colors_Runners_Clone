using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class InputData
    {
        public float PlayerInputSpeed = 2f;
        public Vector3 ClampSides = new Vector2(-3, 3);
        public float ClampSpeed = 0.007f;
    }
}
