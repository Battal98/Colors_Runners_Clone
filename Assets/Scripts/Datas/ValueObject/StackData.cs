using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class StackData
    {

        [Header("StacData"), Space(10)] 
        public int StackLimit = 10;
        
        [Header("Animation Value"),Space(10)]
        public float StackTaskDelay = 0.05f;
        public float StackMaxScaleValue = 1.5f; 
        
        [Header("Lerp Value"),Space(10)]
        public float StackScaleDelay = 0.30f;
        public float StackLerpXDelay = 0.30f;
        public float StackLerpYDelay = 0.30f;
        public float StackLerpZDelay = 0.30f;
        public float StackOffset = 0.30f;
        
        [Header("Jump Value"),Space(10)]
        public float StackJumpDistance = 100f;
        public float StackJumpDuration = 1f;
    }
}
