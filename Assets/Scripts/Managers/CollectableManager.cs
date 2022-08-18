using Commands;
using Controller;
using Controller.Collectable;
using Data.UnityObject;
using Enums;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorType CollectableColorType;
        public Material CollectableMaterialData;

        #endregion

        #region Serialized Variables

        [SerializeField] private CollectableAnimationController collectableAnimationController;
        [SerializeField] private CollectableMeshController collectableMeshController;
        [SerializeField] private CollectableManager collectableManager;

        #endregion

        #region Private Variables
        
        private CollectableColorCheckCommand _collectableColorCheckCommand;

        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void GetReferences()
        {
            CollectableMaterialData = GetCollectableData();
        }

        private void Init()
        {
            _collectableColorCheckCommand = new CollectableColorCheckCommand(ref collectableManager);
           
        }

        private void Start()
        {
            collectableMeshController.CollectableMaterial(CollectableMaterialData);
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
            return Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData
                .CollectableMaterialList[(int)CollectableColorType];
        }

        public void CollectableColorChange(Material colorType)
        {
            collectableMeshController.CollectableMaterial(colorType);
        }
    }
}