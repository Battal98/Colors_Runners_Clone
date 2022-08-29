using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class DroneController : MonoBehaviour
    {
        #region Private Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Animator doTweenPath;

        #endregion

        
        public void DroneMove()
        {
           doTweenPath.SetTrigger("Drone");
        }
    }
}