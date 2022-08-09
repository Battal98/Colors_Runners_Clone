using System;
using UnityEngine;
using Signals;
using System.Collections.Generic;
using Managers;

namespace Controller.Collectable
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool isTaken;

        #endregion

        #region Serializable Variables
        
        
        [SerializeField] private CollectableManager _collectableManager;
        #endregion

        #region Private Variables
        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                StackSignals.Instance.onIncreaseStack?.Invoke(other.gameObject);
            }
        }
    }
}
