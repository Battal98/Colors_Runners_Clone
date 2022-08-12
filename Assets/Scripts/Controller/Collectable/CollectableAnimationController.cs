using System;
using Enums;
using Managers;
using UnityEngine;

namespace Controller
{
    public class CollectableAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private Animator animatorController;
      
        
        #endregion

        #region Private Variables

        [SerializeField]
        private CollectableAnimationStates _states = CollectableAnimationStates.Idle;

        

        #endregion
        #endregion


        private void Start()
        {
            Playanim(_states);
        }

        public void Playanim(CollectableAnimationStates animationStates)
        {
            animatorController.SetTrigger(animationStates.ToString());
        }
        public void OnReset()
        {
            animatorController.SetTrigger(PlayerAnimationStates.Idle.ToString());
        }
    }
}