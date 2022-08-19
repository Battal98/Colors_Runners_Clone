using System.Collections.Generic;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controller
{
    public class ColorCheckAreaPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        [SerializeField] private ColorCheckAreaManager colorCheckAreaManager;
        [SerializeField] private Transform colHolder;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void TurretAreaJobs()
        {
            //ColorCheckAreaSignals.Instance.onChangeJobsOnColorArea(ColorCheckAreaType.Turret);
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
            {
                switch (colorCheckAreaManager.AreaType)
                {
                    case ColorCheckAreaType.Drone:

                        DroneAreaJobs(other);
                        break;

                    case ColorCheckAreaType.Turret:

                        TurretAreaJobs();
                        break;
                }
            }
        }
    }
}