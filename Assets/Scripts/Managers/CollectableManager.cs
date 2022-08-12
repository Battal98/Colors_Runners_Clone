using Controller;
using UnityEngine;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
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

        [SerializeField] private CollectableAnimationController collectableAnimationController;

        
        #endregion
        
        #region Private Variables
        
        #endregion
        
        #endregion

      
        private void Awake()
        {
            CollectableData = GetCollectableData();
        }


        public void SetAnim(CollectableAnimationStates states)
        {
            collectableAnimationController.Playanim(states);
            
        }
        
        private CollectableData GetCollectableData()
        {
            return Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData;
        }

    }
}
