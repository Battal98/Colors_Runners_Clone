using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PoolGenerator", menuName = "ColorsRunners/CD_PoolGenerator", order = 0)]
    public class CD_PoolGenerator : ScriptableObject
    {
        public List<PoolData> PoolObjectList;
    }
}