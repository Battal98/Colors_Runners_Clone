using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_BuildData", menuName = "IdleGame/CD_BuildData", order = 0)]
    public class CD_BuildData : ScriptableObject
    {
        public List<BuildData> BuildData;
    }
}