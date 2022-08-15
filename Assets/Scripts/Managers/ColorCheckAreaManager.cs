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

        public ColorCheckAreaType areaType;

        #endregion

        #region Seriazible Variables

        [SerializeField] private GameObject turret;
        [SerializeField] private GameObject drone;
        [SerializeField] private ColorCheckAreaManager _colorCheckAreaManager;
        [SerializeField] private DroneController droneController;
        [SerializeField] private List<TurretController> turretController;
        [SerializeField] private List<ColorCheckPhysicController> colorCheckPhysicControllers;

        #endregion

        #region Private Variables

        private OutLineChangeCommand _outLineChangeCommand;
        private DroneCheckCountCommand _droneCheckCountCommand;
        private GameObject _platformCheck;

        #endregion

        #endregion


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ColorCheckAreaSignals.Instance.onTurretActive += OnTurretActive;
            ColorCheckAreaSignals.Instance.onDroneActive += OnDroneActive;
            ColorCheckAreaSignals.Instance.onInteractionColorCheck += OnInteractionColorCheck;
            ColorCheckAreaSignals.Instance.onCheckAreaControl += OnCheckAreaControl;

            StackSignals.Instance.onStackTransferComplete += OnCheckStackCount;


        }

        private void UnsubscribeEvents()
        {
            ColorCheckAreaSignals.Instance.onTurretActive -= OnTurretActive;
            ColorCheckAreaSignals.Instance.onDroneActive -= OnDroneActive;
            ColorCheckAreaSignals.Instance.onInteractionColorCheck -= OnInteractionColorCheck;
           
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
        }

        private void GetReferences()
        {
            switch (areaType)
            {
                case ColorCheckAreaType.Drone:
                    OnDroneActive();
                    break;
                case ColorCheckAreaType.Turret:
                    OnTurretActive();
                    break;
            }

            _outLineChangeCommand = new OutLineChangeCommand( );
            _droneCheckCountCommand = new DroneCheckCountCommand(ref colorCheckPhysicControllers,ref _colorCheckAreaManager);
        }
        private void OnTurretActive()
        {
            if (!turret.activeInHierarchy)
                turret.SetActive(true);
            drone.SetActive(false);
        }
        private void OnDroneActive()
        {
            if (!drone.activeInHierarchy)
                drone.SetActive(true);
            turret.SetActive(false);
        }
        private void OnInteractionColorCheck(GameObject _obj)   
        {
        }
        public void PlayDroneAnim()
        {
            droneController.DroneMove();
        }
        public void SetTargetForTurrets(Transform target, bool isPlayerDetected)
        {
            for (var i = 0; i < turretController.Count; i++)
            {
                //target = FindObjectOfType<PlayerManager>().gameObject.transform;
                turretController[i].targetPlayer = target.transform;
                turretController[i].isTargetPlayer = isPlayerDetected;
            }
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

     
    }
}