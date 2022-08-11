using UnityEngine;
using Signals;
using Managers;

namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool isTaken;

        #endregion

        #region Serializable Variables
        
        [SerializeField] private CollectableManager _collectableManager;
        
        #endregion

        #region Private Variables
        
        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && isTaken)
            {
                var otherPhysic = other.gameObject.GetComponent<CollectablePhysicsController>();
                if (!otherPhysic.isTaken)
                {
                    otherPhysic.isTaken = true;
                    StackSignals.Instance.onIncreaseStack?.Invoke(other.gameObject.transform.parent.gameObject);
                }
            }
        }
    }
}
