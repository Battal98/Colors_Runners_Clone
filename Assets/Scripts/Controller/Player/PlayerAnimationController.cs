using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animatorController;

        #endregion

        #endregion

        public void PlayAnim(float Value)
        {
            animatorController.SetFloat("Speed",Value);
            
        }

       
     
    }
}