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
        
        public Material CollectableMaterial;
        
        
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
            CollectableMaterial = GetCollectableData();
            collectableMeshController.CollectableMaterial(CollectableMaterial);
        }


        public void SetAnim(CollectableAnimationStates states)
        {
            collectableAnimationController.Playanim(states);
            
        }
        
        private Material GetCollectableData()
        {
            return Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData.CollectableMaterialList[(int)collectableColorType];
        }

        public void CollectableColorChange(Material colorType)
        {
            collectableMeshController.CollectableMaterial(colorType);
        }

    }
}
