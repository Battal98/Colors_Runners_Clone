using System;
using System.Collections.Generic;
using Commands;
using Controller;
using Controllers;
using Enums;
using Signals;
using UnityEngine;
using Data.UnityObject;
using Data.ValueObject;

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
    

        #endregion

        #region Private Variables

        private OutLineChangeCommand _outLineChangeCommand;
        private CollectablePositionSetCommand _collectablePositionSetCommand;

        #endregion

        #endregion


        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }
        
        private void Subscribe()
        {
            ColorCheckAreaSignals.Instance.onSetCollectableOutline += _outLineChangeCommand.Execute;
            ColorCheckAreaSignals.Instance.onChangeJobsOnColorArea += OnChangeJobsColorArea;

        }
        private void UnSubscribe()
        {
            ColorCheckAreaSignals.Instance.onSetCollectableOutline -=  _outLineChangeCommand.Execute;
            ColorCheckAreaSignals.Instance.onChangeJobsOnColorArea -= OnChangeJobsColorArea;

        }

       
        private void OnDisable()
        {
            UnSubscribe();
        }

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
            _outLineChangeCommand = new OutLineChangeCommand(ref ColorCheckAreaStackList);
            _collectablePositionSetCommand = new CollectablePositionSetCommand();
        }

        private void OnChangeJobsColorArea(ColorCheckAreaType areaType)
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