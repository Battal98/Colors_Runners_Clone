using UnityEngine;
using Signals;
using Managers;
using Enums;


namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool isTaken;

        #endregion

        #region Serializable Variables

        [SerializeField] private CollectableManager collectablemanager;

        #endregion

        #region Private Variables

        private ColorCheckAreaType _areaType;
        private int _timer;

        #endregion

        #endregion

        private void CheckAreaEnter(ColorCheckAreaType areaType)
        {
            if (areaType == ColorCheckAreaType.Turret)
            {
                collectablemanager.SetAnim(CollectableAnimationStates.CrouchWalk);
            }
        }

        private void ColorAreaExit(ColorCheckAreaType areaType)
        {
            if (areaType == ColorCheckAreaType.Turret)
            {
                collectablemanager.SetAnim(CollectableAnimationStates.Run);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && isTaken)
            {
                var otherPhysic = other.gameObject.GetComponent<CollectablePhysicsController>();
                if (!otherPhysic.isTaken)
                {
                    otherPhysic.isTaken = true;
                    collectablemanager.CollectableColorCheck(other.transform.parent.gameObject);
                }
            }

            if (other.CompareTag("CheckArea"))
            {
                _areaType = other.GetComponentInParent<MiniGameAreaManager>().AreaType;
                CheckAreaEnter(_areaType);
            }

            if (other.CompareTag("Gate"))
            {
                var otherColorType = other.GetComponentInParent<GateManager>().ColorType;
                other.GetComponent<Collider>().enabled = false;
                StackSignals.Instance.onChangeCollectableColor?.Invoke(otherColorType);
            }


            if (other.CompareTag("Obstacle") && isTaken)
            {
                other.gameObject.SetActive(false);
                StackSignals.Instance.onRemoveInStack?.Invoke(transform.parent.gameObject);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CheckArea"))
            {
                ColorAreaExit(_areaType);
            }
        }
    }
}