using System;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public struct PoolData
    {
        public string ObjName;
        public GameObject Pref;
        public int ObjectCount;
    }
}