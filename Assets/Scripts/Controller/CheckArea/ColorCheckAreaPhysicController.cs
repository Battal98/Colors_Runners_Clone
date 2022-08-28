using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controller
{
    public class ColorCheckAreaPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ColorCheckAreaManager colorCheckAreaManager;
        [SerializeField] private Transform colHolder;

        #endregion

        #endregion

        private void TurretAreaJobs()
        {
            colorCheckAreaManager.ChangeJobsColorArea(ColorCheckAreaType.Turret);
        }

        private void DroneAreaJobs(Collider other)
        {
            StackSignals.Instance.onTransportInStack?.Invoke(other.transform.parent.gameObject, colHolder);
            colorCheckAreaManager.ColorCheckAreaStackList.Add(other.transform.parent.gameObject);
            other.gameObject.GetComponent<Collider>().enabled = false;
            colorCheckAreaManager.MoveCollectablesToArea(other.transform.parent.gameObject, colHolder);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
                if (colorCheckAreaManager.AreaType == ColorCheckAreaType.Drone)
                    DroneAreaJobs(other);

            if (other.CompareTag("Player"))
                if (colorCheckAreaManager.AreaType == ColorCheckAreaType.Turret)
                    TurretAreaJobs();
        }
    }
}