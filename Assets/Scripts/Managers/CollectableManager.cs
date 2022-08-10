using System;
using System.Collections;
using UnityEngine;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Player.Controllers;
using TMPro;
using Signals;
namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        public CollectableData CollectableData;
        #endregion
        
        #region Serializable Variables
       
        #endregion
        
        #region Private Variables
        #endregion
        
        #endregion

        #region Event Subscription

        private void Awake()
        {
            CollectableData = GetCollectableData();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;

        }
        
        private void UnsubscribeEvents()
        { 
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        } 
        

        #endregion

        private void OnIncreaseStack(GameObject other)
        {
            AddOnStack(other);
        }

        private void AddOnStack(GameObject other)
        {
            if (other.CompareTag("Collectable"))
            {
                other.CompareTag("Collected");
                
                //other.transform.localPosition = new Vector3(0, 0, -0.5f);
                
            }
        }
        private CollectableData GetCollectableData()
        {
            return Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData;
        }

    }
}
