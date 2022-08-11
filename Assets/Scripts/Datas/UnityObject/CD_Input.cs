using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Input", menuName = "ColorsRunners/CD_Input", order = 0)]
    public class CD_Input : ScriptableObject
    {
        public InputData InputData;
    }
}