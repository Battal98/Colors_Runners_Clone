using Enums;
using UnityEngine;

namespace Controllers
{
    public class CollectableAnimationController : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables

        public CollectableAnimationStates State = CollectableAnimationStates.Idle;

        #endregion

        #region Serialized Variables
        
        [SerializeField] private Animator animatorController;
        
        #endregion

        #region Private Variables

        #endregion

        #endregion
        
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