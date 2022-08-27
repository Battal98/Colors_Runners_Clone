using System;
using Enums;

namespace Datas.ValueObject
{
    [Serializable]
    public struct AreaData
    {
        public float GardenMaterialValue;
        public float BuildMaterialValue;
        public AreaStageType Type;
    }
}