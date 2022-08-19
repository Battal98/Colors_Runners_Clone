using System.Collections.Generic;
using Commands;
using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class MiniGameAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorCheckAreaType AreaType;
        public List<TurretController> turretController;

        #endregion

        #region Seriazible Variables

        [SerializeField] private GameObject turret;
        [SerializeField] private GameObject drone;
        [SerializeField] private MiniGameAreaManager miniGameAreaManager;
        [SerializeField] private DroneController droneController;

        #endregion

        #region Private Variables

        private DroneSquencePlayCommand _droneSquencePlayCommand;
        private SetTurretTargetCommand _setTurretTarget;
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
            StackSignals.Instance.onStackTransferComplete += OnOnEnterInDroneArea;
        }

        private void UnsubscribeEvents()
        {
            ColorCheckAreaSignals.Instance.onCheckAreaControl -= OnCheckAreaControl;
            StackSignals.Instance.onStackTransferComplete -= OnOnEnterInDroneArea;
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
            _droneSquencePlayCommand = new DroneSquencePlayCommand(ref miniGameAreaManager);
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

        private void OnOnEnterInDroneArea()
        {
            if (transform.gameObject == _platformCheck)
            {
                StartCoroutine(_droneSquencePlayCommand.Execute());
            }
        }

        private void OnCheckAreaControl(GameObject other)
        {
            _platformCheck = other;
        }
    }
}