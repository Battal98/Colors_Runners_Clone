using System;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public struct BuildData
    {
        public string BuildName;
        public float BuildCost;
        public float GardenCost;
        public Material BuildMaterial;
        public Material GardenMaterial;
    }
}