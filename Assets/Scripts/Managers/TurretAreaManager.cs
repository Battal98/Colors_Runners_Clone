using System.Collections.Generic;
using Controllers;
using Signals;
using UnityEngine;

namespace Managers
{
    public class TurretAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private GameObject _platformCheck;

        #endregion

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private List<ColorCheckAreaManager> colorCheckAreaManagers;
        public List<TurretController> turretController;

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
            ColorCheckAreaSignals.Instance.onTurretIsActive += OnTurretIsActive;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            ColorCheckAreaSignals.Instance.onCheckAreaControl -= OnCheckAreaControl;
            ColorCheckAreaSignals.Instance.onTurretIsActive -= OnTurretIsActive;
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SetTarget()
        {
            var _target = FindObjectOfType<PlayerManager>().transform;
            for (int i = 0; i < turretController.Count; i++)
            {
                turretController[i].SetTarget(_target);
            }
        }

        private void OnTurretIsActive(bool isCheck)
        {
            if (_platformCheck == gameObject)
            {
                for (int i = 0; i < turretController.Count; i++)
                {
                    turretController[i].isTargetPlayer = isCheck;
                }
            }
        }

        private void OnCheckAreaControl(GameObject other)
        {
            _platformCheck = other;
        }

        private void OnPlay()
        {
            SetTarget();
        }
    }
}