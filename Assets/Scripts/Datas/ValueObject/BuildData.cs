using System;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public struct BuildData
    {
        public string BuildName;
        public int BuildCost;
        public int GardenCost;
        public Material BuildMaterial;
        public Material GardenMaterial;
    }
}