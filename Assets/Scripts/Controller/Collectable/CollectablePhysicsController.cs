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
        
        [SerializeField] private CollectableManager collectablemanager;
       
        #endregion

        #region Private Variables


        #endregion

        #endregion

        private  void CheckAreaEnter(Collider other,ColorCheckAreaType areaType)
        {
          
            if ( areaType== ColorCheckAreaType.Turret)
            { 
                collectablemanager.SetAnim(CollectableAnimationStates.CrouchWalk);
            }
        }
        private  void ColorAreaExit(Collider other,ColorCheckAreaType areaType)
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
                var areaType = other.GetComponentInParent<ColorCheckAreaManager>().AreaType;
                CheckAreaEnter(other,areaType);
            }

            if (other.CompareTag("Gate"))
            {
                var otherMR = other.gameObject.transform.parent.GetComponentInChildren<MeshRenderer>();
                other.GetComponent<Collider>().enabled = false;
                StackSignals.Instance.onChangeCollectableColor?.Invoke(otherMR.material);
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
                var areaType = other.GetComponentInParent<ColorCheckAreaManager>().AreaType;
                ColorAreaExit(other,areaType);
            }
        }

      
    }
    
}