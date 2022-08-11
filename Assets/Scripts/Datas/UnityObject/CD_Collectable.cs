using Datas.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Collectable", menuName = "ColorsRunners/CD_Collectable", order = 0)]
    public class CD_Collectable : ScriptableObject
    {
        public CollectableData CollectableData;
    }
}