using System.Collections.Generic;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "MovementList", menuName = "Movement/MovementList", order = 0)]
    public class CD_MovementList : ScriptableObject
    {
        public List<CD_Movement> MovementTypeList;

    }
    
}