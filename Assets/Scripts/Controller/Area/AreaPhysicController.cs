using System;
using Managers;
using UnityEngine;

namespace Controller
{
    public class AreaPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private AreaManager _manager;

        #endregion

        #region Private Variables
        

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                other.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}