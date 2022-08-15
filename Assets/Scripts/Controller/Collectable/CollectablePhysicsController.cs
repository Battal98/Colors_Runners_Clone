using System;
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
        [SerializeField] private SkinnedMeshRenderer collectableSkinnedMeshRenderer;
        [SerializeField] private CollectableAnimationController collectableAnimationController;

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
                    collectablemanager.CollectableColorCheck(other.transform.parent.gameObject);

                }
              
            }

            if (other.CompareTag("Gate"))
            {
                var otherMR = other.gameObject.transform.parent.GetComponentInChildren<MeshRenderer>();
                collectablemanager.CollectableMaterial = otherMR.material;
                collectableSkinnedMeshRenderer.material.color = otherMR.material.color;
            }


            if (other.CompareTag("Obstacle") && isTaken)
            {
                other.gameObject.SetActive(false);
                StackSignals.Instance.onRemoveInStack?.Invoke(transform.parent.gameObject);
            }

        }

    }
}