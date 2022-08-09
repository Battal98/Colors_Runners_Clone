using System;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class StackData : MonoBehaviour
    {
        public GameObject CollectableObject;
        public List<GameObject> StackList = new List<GameObject>();
        public float StackTaskDelay = 0.05f;
        public float StackMaxScaleValue = 1.5f; 
        public float StackScaleDelay = 0.30f;
        public float StackLerpDelay = 0.30f;
    }
}