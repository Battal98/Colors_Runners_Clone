using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Animator animatorController;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        public void Playanim(PlayerAnimationStates animationStates)
        {
            animatorController.SetTrigger(animationStates.ToString());
        }

        public void OnReset()
        {
            animatorController.SetTrigger(PlayerAnimationStates.Idle.ToString());
        }
    }
}