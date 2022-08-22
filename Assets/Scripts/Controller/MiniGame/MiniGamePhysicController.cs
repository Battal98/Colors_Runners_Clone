using System;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class MiniGamePhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private MiniGameAreaManager miniGameAreaManager;

        #endregion

        #region Private Variables

        #endregion

        #endregion


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
               
                    miniGameAreaManager.TurretIsActive(false);
                
              
            }
        }
    }
}