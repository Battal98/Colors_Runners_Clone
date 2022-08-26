using System;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public struct IdleData
    {
        [Header("Data"), Space(15)] 
        public int BuildCount;
        
    }
}