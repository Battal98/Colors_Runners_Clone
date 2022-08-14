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
        [SerializeField] private CollectableManager collectablemanager;

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
                else
                {
                 
                }
            }

            if (other.CompareTag("Gate"))
            {
                var otherMR = other.gameObject.transform.parent.GetComponentInChildren<MeshRenderer>();
                _collectableSkinnedMeshRenderer.material.color = otherMR.material.color;
            }


            if (other.CompareTag("Obstacle") && isTaken)
            {
                other.gameObject.SetActive(false);
                StackSignals.Instance.onRemoveInStack?.Invoke(transform.parent.gameObject);
            }

            if (other.CompareTag("CheckArea"))
            {
                Debug.Log("Deneme");
                ColorCheckAreaSignals.Instance.onCheckStackCount?.Invoke();
            }
        }

    }
}