using Enums;
using UnityEngine;

namespace Controller
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
        
        public void PlayCollectableAnim(PlayerAnimationStates animationStates)
        {
            animatorController.SetTrigger(animationStates.ToString());
        }
    }
}
