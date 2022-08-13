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
             
                var otherPhysic = other.gameObject.GetComponent<CollectablePhysicsController>();
                if (!otherPhysic.isTaken)
                {
                    otherPhysic.isTaken = true;
                    StackSignals.Instance.onAddInStack?.Invoke(other.transform.parent.gameObject);
                }
            }

            if (other.CompareTag("Gate"))
            {
                var otherMR = other.gameObject.transform.parent.GetComponentInChildren<MeshRenderer>();
                _collectableSkinnedMeshRenderer.material.color = otherMR.material.color;
            }

            // if (other.CompareTag("CheckArea"))
            // {
            //     var type = other.gameObject.GetComponentInParent<ColorCheckAreaManager>().areaType;
            //     switch (type)
            //     {
            //         case ColorCheckAreaType.Drone:
            //             CollectablesMovementInDrone();
            //             break;
            //         case ColorCheckAreaType.Turret:
            //             //change animation state 
            //             break;
            //     }
            // }
            if (other.CompareTag("ColorCheck"))
            {
                CollectablesMovementInColorCheckArea(other.gameObject);
            }
            // if (other.CompareTag("JumpArea") && CompareTag("Collectable"))
            // {
            //     StackSignals.Instance.onStackJumpPlatform?.Invoke();
            // }


            if (other.CompareTag("Obstacle") && isTaken)
            {
                other.gameObject.SetActive(false);
                StackSignals.Instance.onRemoveInStack?.Invoke(transform.parent.gameObject);
            }
        }


        private void CollectablesMovementInDrone()
        {
            //ColorCheckAreaSignals.Instance.onDroneActive?.Invoke();

            //StackSignals.Instance.onDecreaseStack?.Invoke(this.gameObject);
        }

        private void CollectablesMovementInColorCheckArea(GameObject other)
        {
            StackSignals.Instance.onTransportInStack?.Invoke(transform.parent.gameObject);
            //animation is working but not working correctly :')
            var randomValue = Random.Range(-1f, 1f);
            transform.parent.transform
                .DOMove(
                    new Vector3(other.transform.position.x, transform.parent.transform.position.y,
                        other.transform.position.z + randomValue), 0.50f).OnComplete(() =>
                    manager.SetAnim(CollectableAnimationStates.Crouch));
        }
    }
}