using Commands;
using Controller;
using Data.UnityObject;
using Enums;
using UnityEngine;
using Data.ValueObject;
using System.Collections.Generic;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorType CollectableColorType;
        [HideInInspector] public List<ColorData> Datas;

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
            Datas = GetColorData();
            Init();
        }

        private List<ColorData> GetColorData()
        {
            return Resources.Load<CD_Color>("Data/CD_Color").Data;
        }

        private void Init()
        {
            _collectableColorCheckCommand = new CollectableColorCheckCommand(ref collectableManager);
        }

        private void Start()
        {
            collectableMeshController.ChangeCollectableColor(CollectableColorType);
        }

        public void SetAnim(CollectableAnimationStates states)
        {
            collectableAnimationController.Playanim(states);
        }

        public void CollectableColorCheck(GameObject other)
        {
            _collectableColorCheckCommand.Exucute(other);
        }

        // private Material GetCollectableData()
        // {
        //     return Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData
        //         .CollectableMaterialList[(int)CollectableColorType];
        // }

        public void CollectableColorChange(ColorType colorType)
        {
            collectableMeshController.ChangeCollectableColor(colorType);
        }
    }
}