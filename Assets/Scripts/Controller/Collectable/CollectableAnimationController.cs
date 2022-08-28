using Enums;
using UnityEngine;

namespace Controller
{
    public class CollectableAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animatorController;

        #endregion

        #endregion


        public void Playanim(CollectableAnimationStates animationStates)
        {
            animatorController.SetTrigger(animationStates.ToString());
        }
    }
}