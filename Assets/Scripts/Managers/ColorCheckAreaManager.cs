using UnityEngine;
using Signals;
using Keys;
using Enums;
using Controllers;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

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
            ColorCheckAreaSignals.Instance.onCheckStackCount += OnCheckStackCount;
        }

        private void UnsubscribeEvents()
        {
            ColorCheckAreaSignals.Instance.onTurretActive -= OnTurretActive;
            ColorCheckAreaSignals.Instance.onDroneActive -= OnDroneActive;
            ColorCheckAreaSignals.Instance.onInteractionColorCheck -= OnInteractionColorCheck;
            ColorCheckAreaSignals.Instance.onCheckStackCount -= OnCheckStackCount;
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

        private int GetStackCount()
        {
            return StackSignals.Instance.onSendStackCount.Invoke();
        }
        private void OnCheckStackCount()
        {
            StartCoroutine(CheckCount());
        }
        private IEnumerator CheckCount()
        {
            Debug.Log(GetStackCount());
            if (GetStackCount() <= 1)
            {
                Debug.Log("In");
                #region Collectable Material Outline Jobs

                //OutlineJobs(0);

                #endregion

                #region Drone Movement and Color Check Jobs

                CameraSignals.Instance.onSetCameraTarget?.Invoke(null);
                yield return new WaitForSeconds(.5f); // wait for before drone movement 

                PlayDroneAnim();

                yield return new WaitForSeconds(7.5f / 2f);// kill wrong collectables
                //CheckColor();
                yield return new WaitForSeconds(7.5f / 2f);// wait for drone movement
                var _playerManager = FindObjectOfType<PlayerManager>();
                //AfterDroneMovementJobs(_playerManager);
                #endregion
            }
        }

       /* private void OutlineJobs(float endValue)
        {
            for (int i = 0; i < stackList.Count; i++)
            {
                var materialColor = stackList[i].GetComponentInChildren<SkinnedMeshRenderer>().material;
                materialColor.DOFloat(endValue, "_OutlineSize", 0.5f);
            }

        }

        private void AfterDroneMovementJobs(PlayerManager _playerManager)
        {
            StackSignals.Instance.onGetStackList?.Invoke(stackList);
            CameraSignals.Instance.onSetCameraTarget?.Invoke(_playerManager.transform);
            _playerManager.transform.DOMoveZ(_playerManager.transform.position.z + 2.9f, .5f);
            CoreGameSignals.Instance.onPlayerChangeForwardSpeed?.Invoke(1);
            OutlineJobs(15);
        }*/

    }
}
