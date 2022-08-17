using System.Collections;
using System.Collections.Generic;
using Commands;
using Controllers;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ColorCheckAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorCheckAreaType AreaType;

        #endregion

        #region Seriazible Variables

        [SerializeField] private GameObject turret;
        [SerializeField] private GameObject drone;
        [SerializeField] private ColorCheckAreaManager colorCheckAreaManager;
        [SerializeField] private DroneController droneController;
        [SerializeField] private List<TurretController> turretController;
        [SerializeField] private List<ColorCheckPhysicController> colorCheckPhysicControllers;

        #endregion

        #region Private Variables

        private OutLineChangeCommand _outLineChangeCommand;
        private DroneCheckCountCommand _droneCheckCountCommand;
        private MoveCollectableToCheckAreaCommand _moveCollectableToCheckAreaCommand;
        private SetTargetForTurretCommand _setTargetForTurret;
        private GameObject _platformCheck;
        private Transform _target;

        #endregion

        #endregion


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ColorCheckAreaSignals.Instance.onCheckAreaControl += OnCheckAreaControl;

            StackSignals.Instance.onStackTransferComplete += OnCheckStackCount;


        }

        private void UnsubscribeEvents()
        {
           
            ColorCheckAreaSignals.Instance.onCheckAreaControl -= OnCheckAreaControl;
            StackSignals.Instance.onStackTransferComplete -= OnCheckStackCount;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void GetReferences()
        {
            switch (AreaType)
            {
                case ColorCheckAreaType.Drone:
                    DroneActive();
                    break;
                case ColorCheckAreaType.Turret:
                    TurretActive();
                    break;
            }

            _target = FindObjectOfType<PlayerManager>().gameObject.transform;
        }

        private void Init()
        {
            _outLineChangeCommand = new OutLineChangeCommand();
            _moveCollectableToCheckAreaCommand = new MoveCollectableToCheckAreaCommand();
            _droneCheckCountCommand = new DroneCheckCountCommand(ref colorCheckPhysicControllers, ref colorCheckAreaManager);
            _setTargetForTurret = new SetTargetForTurretCommand(ref turretController, ref colorCheckPhysicControllers);
        }
        private void TurretActive()
        {
            if (!turret.activeInHierarchy)
                turret.SetActive(true);
            drone.SetActive(false);
        }
        private void DroneActive()
        {
            if (!drone.activeInHierarchy)
                drone.SetActive(true);
            turret.SetActive(false);
        }

        public void PlayDroneAnim()
        {
            droneController.DroneMove();
        }
        public void SetTargetForTurrets()
        {
            for (var i = 0; i < colorCheckPhysicControllers.Count; i++)
                _setTargetForTurret.Execute(i, _target);
        }
       
        private void OnCheckStackCount()
        {
            if (transform.gameObject==_platformCheck)
            {
                for (var i = 0; i < colorCheckPhysicControllers.Count; i++)
                    StartCoroutine(_droneCheckCountCommand.Execute(colorCheckPhysicControllers[i].stackList,i));
            }
        }

        public void SetOutline(List<GameObject> stack,float endValue)
        {
            _outLineChangeCommand.Execute(stack,endValue);
        }

        private void OnCheckAreaControl(GameObject other)
        {
            _platformCheck = other;
        }

        #region Collectable Movement To Color Check Area

        public void MoveCollectablesToArea(GameObject other, Transform _colHolder)
        {
            _moveCollectableToCheckAreaCommand.Execute(other, _colHolder);
        }

        #endregion

    }
}