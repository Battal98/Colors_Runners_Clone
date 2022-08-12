using Controller;
using UnityEngine;
using Signals;
using Managers;
using Enums;
using DG.Tweening;

namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool isTaken;

        #endregion

        #region Serializable Variables

   
        [SerializeField] private GameObject collectableMeshObj;
        [SerializeField] private CollectableManager manager;

        #endregion

        #region Private Variables

        [SerializeField] private SkinnedMeshRenderer _collectableSkinnedMeshRenderer;
        [SerializeField] private CollectableAnimationController collectableAnimationController;

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && isTaken)
            {
                manager.SetAnim(CollectableAnimationStates.Run);
                var otherPhysic = other.gameObject.GetComponent<CollectablePhysicsController>();
                if (!otherPhysic.isTaken)
                {
                    otherPhysic.isTaken = true;
                    StackSignals.Instance.onAddInStack?.Invoke(other.gameObject.transform.parent.gameObject);
                }
            }
            if (other.CompareTag("Gate"))
            {
                var otherMR = other.gameObject.transform.parent.GetComponentInChildren<MeshRenderer>();
                _collectableSkinnedMeshRenderer.material.color = otherMR.material.color;
            }
            if (other.CompareTag("CheckArea"))
            {
                var type = other.gameObject.GetComponentInParent<ColorCheckAreaManager>().areaType;
                switch (type)
                {
                    case ColorCheckAreaType.Drone:
                        CollectablesMovementInDrone();
                        break;
                    case ColorCheckAreaType.Turret:
                        //change animation state 
                        break;
                }
            }
            if (other.CompareTag("ColorCheck"))
            {
                StackSignals.Instance.onTransportInStack?.Invoke(transform.parent.gameObject);
                transform.parent.transform.DOMove(new Vector3(other.gameObject.transform.position.x, transform.parent.transform.position.y, other.gameObject.transform.position.z), 0.5f);

            }
        }
        
        private void CollectablesMovementInDrone()
        {
            //ColorCheckAreaSignals.Instance.onDroneActive?.Invoke();

            //StackSignals.Instance.onDecreaseStack?.Invoke(this.gameObject);
        }
    }
}
