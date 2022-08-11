using UnityEngine;
using Data.UnityObject;
using Datas.ValueObject;
using Signals;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        
        public CollectableData CollectableData;
        
        #endregion
        
        #region Serialized Variables
       
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
            StackSignals.Instance.onAddInStack += OnIncreaseStack;

        }
        
        private void UnsubscribeEvents()
        { 
            StackSignals.Instance.onAddInStack -= OnIncreaseStack;
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
            }
        }
        private CollectableData GetCollectableData()
        {
            return Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData;
        }

    }
}
