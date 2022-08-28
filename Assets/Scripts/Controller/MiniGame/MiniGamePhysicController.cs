using Managers;
using UnityEngine;

namespace Controllers
{
    public class MiniGamePhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MiniGameAreaManager miniGameAreaManager;

        #endregion

        #endregion


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) miniGameAreaManager.TurretIsActive(false);
        }
    }
}