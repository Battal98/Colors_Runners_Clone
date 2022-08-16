using System.Collections.Generic;
using Controller;
using Enums;
using Managers;
using UnityEngine;

namespace Commands
{
    public class CollectableAnimSetCommand 
    {
        public void Execute(GameObject collectable,CollectableAnimationStates states)
        {
            CollectableManager _collectableManager = collectable.transform.GetComponent<CollectableManager>();
            _collectableManager.SetAnim(states);    
        }
    }
}