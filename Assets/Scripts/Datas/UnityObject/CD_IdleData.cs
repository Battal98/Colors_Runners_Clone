using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_IdleData", menuName = "IdleGame/CD_IdleData", order = 0)]
    public class CD_IdleData : ScriptableObject
    {
        public List<IdleData> DataList;
    }
}