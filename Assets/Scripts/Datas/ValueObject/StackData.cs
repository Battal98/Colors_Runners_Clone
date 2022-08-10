using System;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class StackData 
    {
        public GameObject CollectableObject;
        public float StackTaskDelay = 0.05f;
        public float StackMaxScaleValue = 1.5f; 
        public float StackScaleDelay = 0.30f;
        public float StackLerpDelay = 0.30f;
        public float StackOffset = 0.30f;
    }
}
