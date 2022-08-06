using Enums;
using UnityEngine;

namespace Player.Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public PlayerAnimationStates State=PlayerAnimationStates.Idle;
        #endregion
        #region Serial Variables

        [SerializeField] private Animator animatorController;
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