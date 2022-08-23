using System.Collections;
using System.Collections.Generic;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;

namespace Controllers
{
    public class ColorCheckAreaMeshController : MonoBehaviour
    {
        #region Self Variables

       

        #region Serialized Variables

        [SerializeField] private Transform colHolder;
        [SerializeField] private ColorCheckAreaManager colorCheckAreaManager;
        
     

        #endregion
        #region Private Variables

        private int _count;
        private MeshRenderer _meshRenderer;
        private bool Cık=true;
        #endregion
        #endregion

        private void Awake()
        {
            GetReferences();
          
        }

        private void GetReferences()
        {
          
            _meshRenderer = transform.parent.GetComponentInChildren<MeshRenderer>();
            _meshRenderer.material.color = colorCheckAreaManager.Datas[(int)colorCheckAreaManager.ColorType].Color;
        }

        public void CheckColorsForDrone()
        {
            if (colorCheckAreaManager.AreaType == ColorCheckAreaType.Drone)
            {
                
                _count = colorCheckAreaManager.ColorCheckAreaStackList.Count;
                colorCheckAreaManager.transform.GetChild(1).gameObject.SetActive(false);
                for (int i = 0; i < _count; i++)
                {
                    var colManager = colHolder.GetChild(0).GetComponent<CollectableManager>();
                    if (colorCheckAreaManager.ColorType == colManager.CollectableColorType)
                    {
                        colHolder.GetChild(0).GetComponentInChildren<Collider>().enabled = true;
                        colorCheckAreaManager.ColorCheckAreaStackList.Remove(colHolder.GetChild(0).gameObject);
                        StackSignals.Instance.onGetStackList?.Invoke(colHolder.GetChild(0).gameObject);
                        colorCheckAreaManager.ColorCheckAreaStackList.TrimExcess();
                    }
                    else
                    {
                        if (gameObject.activeInHierarchy)
                        {
                            transform.DOScaleZ(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));
                        }

                        colManager.SetAnim(CollectableAnimationStates.Dead);
                        colorCheckAreaManager.ColorCheckAreaStackList.Remove(colHolder.GetChild(0).gameObject);
                        PoolSignals.Instance.onSendPool?.Invoke(colHolder.GetChild(0).gameObject,PoolType.Collectable);
                        colorCheckAreaManager.ColorCheckAreaStackList.TrimExcess();
                    }
                }

                if (_count == 0)
                {
                    transform.DOScaleZ(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));
                }
            }
        }

        public void CheckColorForTurrets()
        {
            if (colorCheckAreaManager.ColorType != StackSignals.Instance.onGetColorType())
            {
                colorCheckAreaManager.SetTurretActive(true);
              
            }
            else
            {
                colorCheckAreaManager.SetTurretActive(false);
              

            }
        }

     

    }
}