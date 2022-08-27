using System;
using System.Collections.Generic;
using Datas.ValueObject;
using Sirenix.Serialization;

namespace Keys
{
    [Serializable]
    public struct IdleDataParams
    {
        public int Score;
        public int CityLevel;
        public Dictionary<int,AreaData> AreaDatas;
        public int CompletedArea;
    }
}