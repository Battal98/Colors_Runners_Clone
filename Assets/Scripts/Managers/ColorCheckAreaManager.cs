using UnityEngine;
using Signals;
using Keys;
using Enums;

namespace Managers
{
    public class ColorCheckAreaManager : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables

        public ColorCheckAreaType areaType;

        #endregion

        #region Seriazible Variables


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

        private void OnTurretActive()
        {

        }
        private void OnDroneActive()
        {

        }
        private void OnInteractionColorCheck(GameObject _obj)
        {

        }

    }
}
