using UnityEngine;
using Signals;
using Keys;
using Enums;
using Controllers;
using System.Collections.Generic;

namespace Managers
{
    public class ColorCheckAreaManager : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables

        public ColorCheckAreaType areaType;

        #endregion

        #region Seriazible Variables

        [SerializeField]
        private GameObject turret;
        [SerializeField]
        private GameObject drone;
        [SerializeField]
        private DroneController droneController;
        [SerializeField]
        private List<TurretController> turretController;

        #endregion

        #region Private Variables

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
        }

        private void UnsubscribeEvents()
        {
            ColorCheckAreaSignals.Instance.onTurretActive -= OnTurretActive;
            ColorCheckAreaSignals.Instance.onDroneActive -= OnDroneActive;
            ColorCheckAreaSignals.Instance.onInteractionColorCheck -= OnInteractionColorCheck;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Awake()
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
        }

        private void OnTurretActive()
        {
            if(!turret.activeInHierarchy)
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
            for (int i = 0; i < turretController.Count; i++)
            {
                //target = FindObjectOfType<PlayerManager>().gameObject.transform;
                turretController[i].targetPlayer = target.transform;
                turretController[i].isTargetPlayer = isPlayerDetected;
                

            }
        }

    }
}
