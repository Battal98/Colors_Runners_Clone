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

        #endregion

        #region Serialized Variables

        [SerializeField] private TurretController turretController;
        [SerializeField] private GameObject turret;
        [SerializeField] private GameObject drone;
        [SerializeField] private MiniGameAreaManager miniGameAreaManager;
        [SerializeField] private DroneController droneController;
        [SerializeField] private List<ColorCheckAreaManager> colorCheckAreaManagers;

        #endregion

        #region Private Variables

        private DroneSquencePlayCommand _droneSquencePlayCommand;
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
            CoreGameSignals.Instance.onCheckAreaControl += OnCheckAreaControl;
            StackSignals.Instance.onStackTransferComplete += OnOnEnterInDroneArea;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onCheckAreaControl -= OnCheckAreaControl;
            StackSignals.Instance.onStackTransferComplete -= OnOnEnterInDroneArea;
            CoreGameSignals.Instance.onPlay -= OnPlay;
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
        }

        private void Init()
        {
            _droneSquencePlayCommand = new DroneSquencePlayCommand(ref miniGameAreaManager);
        }

        private void OnCheckAreaControl(GameObject other)
        {
            _platformCheck = other;
        }

        #region SetType

        private void TurretActive()
        {
            turret.SetActive(true);
            drone.SetActive(false);
        }

        private void DroneActive()
        {
            drone.SetActive(true);
            turret.SetActive(false);
        }

        #endregion


        #region ForDrone

        private void OnOnEnterInDroneArea()
        {
            if (transform.gameObject == _platformCheck) StartCoroutine(_droneSquencePlayCommand.Execute());
        }

        #endregion

        #region ForTurret

        private void SetTarget()
        {
            var _target = FindObjectOfType<PlayerManager>().transform;
            turretController.SetTarget(_target);
        }

        public void TurretIsActive(bool isCheck)
        {
            if (_platformCheck = gameObject)
                turretController.IsTargetPlayer = isCheck;
        }

        #endregion

        public void PlayDroneAnim()
        {
            droneController.DroneMove();
        }

        public void SetCollectableOutline(float Value)
        {
            for (var i = 0; i < colorCheckAreaManagers.Count; i++)
                colorCheckAreaManagers[i].SetCollectableOutline(Value);
        }

        public void ChangeJobType(ColorCheckAreaType checkAreaType)
        {
            for (var i = 0; i < colorCheckAreaManagers.Count; i++)
                colorCheckAreaManagers[i].ChangeJobsColorArea(checkAreaType);
        }


        private void OnPlay()
        {
            SetTarget();
        }
    }
}