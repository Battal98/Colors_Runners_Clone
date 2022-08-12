using System.Collections.Generic;
using Controller;
using Enums;
using UnityEngine;

namespace Commands
{
    public class CollectableAnimSetCommand 
    {
       

        public void Execute(GameObject collectable,CollectableAnimationStates states)
        {
            CollectableAnimationController _collectableAnimationController =
                collectable.transform.GetComponentInChildren<CollectableAnimationController>();
            _collectableAnimationController.Playanim(states);    
            
        }
        
        
    }
}