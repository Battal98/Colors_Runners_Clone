using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;
using UnityEngine.UI;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "ColorsRunners/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData Data;
    }
}