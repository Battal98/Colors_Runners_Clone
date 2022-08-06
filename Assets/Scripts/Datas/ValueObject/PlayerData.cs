using System;
namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData MovementData;
    }
    
    [Serializable]
    public class PlayerMovementData
    {
        public float ForwardSpeed = 5;
        public float SidewaysSpeed = 2;
    }
}