using Enums;
using UnityEngine;

namespace Controller.Stack
{
    public class CollectableAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public CollectableAnimationStates State = CollectableAnimationStates.Idle;

        #endregion

        #region Serialized Variables

        [SerializeField] private Animator _animatorController;

        #endregion

        #endregion
        
        public void PlayCollectableAnim(PlayerAnimationStates animationStates)
        {
            _animatorController.SetTrigger(animationStates.ToString());
        }
    }
}
