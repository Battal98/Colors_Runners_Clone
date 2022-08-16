using System;
using Commands;
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
        [SerializeField] private ColorType collectableColorType;
        [SerializeField] private CollectableManager collectableManager;
        [SerializeField] private SkinnedMeshRenderer skinnedMesh;
        
        #endregion
        
        #region Private Variables

        private CollectableMeshController _collectableMeshController;
        private CollectableColorCheckCommand _collectableColorCheckCommand;
        
        #endregion
        
        #endregion

      
        private void Awake()
        {
            CollectableMaterial = GetCollectableData();
            Init();
           
        }
        private void Init()
        {
            _collectableColorCheckCommand = new CollectableColorCheckCommand(ref collectableManager);
            _collectableMeshController = new CollectableMeshController(ref skinnedMesh,ref collectableManager);
        }
        private void Start()
        {
            _collectableMeshController.CollectableMaterial(CollectableMaterial);

        }
        public void SetAnim(CollectableAnimationStates states)
        {
            collectableAnimationController.Playanim(states);
            
        }

        public void CollectableColorCheck(GameObject other)
        {
            _collectableColorCheckCommand.Exucute(other);
        }
        private Material GetCollectableData()
        {
            return Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData.CollectableMaterialList[(int)collectableColorType];
        }
        public void CollectableColorChange(Material colorType)
        {
            _collectableMeshController.CollectableMaterial(colorType);
        }

    }
}
