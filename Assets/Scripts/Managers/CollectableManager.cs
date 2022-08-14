using Controller;
using Controller.Collectable;
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
        [SerializeField] private CollectableMeshController collectableMeshController;
        [SerializeField] ColorType collectableColorType;
        
        #endregion
        
        #region Private Variables
        
        #endregion
        
        #endregion

      
        private void Awake()
        {
            CollectableData = GetCollectableData();
            collectableMeshController.CollectableMaterial(collectableColorType);
        }


        public void SetAnim(CollectableAnimationStates states)
        {
            collectableAnimationController.Playanim(states);
            
        }
        
        private CollectableData GetCollectableData()
        {
            return Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData;
        }

        public void CollectableColorChange(ColorType colorType)
        {
            collectableMeshController.CollectableMaterial(colorType);
        }

    }
}
