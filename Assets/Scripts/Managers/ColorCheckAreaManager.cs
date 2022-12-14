using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using UnityEngine;

namespace Managers
{
    public class ColorCheckAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorCheckAreaType AreaType = ColorCheckAreaType.Drone;
        public ColorType ColorType = ColorType.Blue;
        public List<GameObject> ColorCheckAreaStackList;
        public List<ColorData> Datas;

        #endregion

        #region Serialized Variables

        [SerializeField] private ColorCheckAreaMeshController colorCheckAreaMeshController;
        [SerializeField] private MiniGameAreaManager miniGameAreaManager;

        #endregion

        #region Private Variables

        private OutLineChangeCommand _outLineChangeCommand;
        private CollectablePositionSetCommand _collectablePositionSetCommand;

        #endregion

        #endregion


        private void Awake()
        {
            Datas = GetColorData();
            Init();
        }

        private void Start()
        {
            AreaType = miniGameAreaManager.AreaType;
        }

        private List<ColorData> GetColorData()
        {
            return Resources.Load<CD_Color>("Data/CD_Color").Data;
        }

        private void Init()
        {
            _outLineChangeCommand = new OutLineChangeCommand(ref ColorCheckAreaStackList);
            _collectablePositionSetCommand = new CollectablePositionSetCommand();
        }


        public void SetCollectableOutline(float value)
        {
            _outLineChangeCommand.Execute(value);
        }

        public void SetTurretActive(bool IsActive)
        {
            miniGameAreaManager.TurretIsActive(IsActive);
        }

        public void ChangeJobsColorArea(ColorCheckAreaType areaType)
        {
            switch (areaType)
            {
                case ColorCheckAreaType.Drone:
                    colorCheckAreaMeshController.CheckColorsForDrone();
                    break;
                case ColorCheckAreaType.Turret:
                    colorCheckAreaMeshController.CheckColorForTurrets();
                    break;
            }
        }


        public void MoveCollectablesToArea(GameObject other, Transform _colHolder)
        {
            _collectablePositionSetCommand.Execute(other, _colHolder);
        }
    }
}