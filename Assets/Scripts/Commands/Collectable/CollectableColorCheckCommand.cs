using Datas.ValueObject;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class CollectableColorCheckCommand 
    {

        #region Self Variables
        #region Private Variables

        private CollectableManager _manager;
        

        #endregion
        #endregion
        public CollectableColorCheckCommand(ref CollectableManager manager)
        {
            _manager=manager;
            
        }

        public void Exucute(GameObject other)
        {
            if (ColorUtility.ToHtmlStringRGB(other.GetComponent<CollectableManager>().CollectableMaterial.color) ==
                ColorUtility.ToHtmlStringRGB(_manager.CollectableMaterial.color))
            {
                StackSignals.Instance.onAddInStack?.Invoke(other);

            }
            else
            {
                other.SetActive(false);
                StackSignals.Instance.onRemoveInStack?.Invoke(_manager.transform.gameObject);
            }
        }
        
        
        
    }
}