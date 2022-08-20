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

        #region Private Variables

        private int _count;
        private MeshRenderer _meshRenderer;

        #endregion

        #region Serialized Variables

        [SerializeField] private Transform colHolder;
        [SerializeField] private ColorCheckAreaManager colorCheckAreaManager;
     

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            SetColorMaterial();
        }

        private void GetReferences()
        {
            _meshRenderer = this.transform.parent.GetComponentInChildren<MeshRenderer>();
        }

        public void CheckColorsForDrone()
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
                        transform.DOScaleZ(0f, 0.5f).OnComplete(() => this.gameObject.SetActive(false));
                    }

                    colManager.SetAnim(CollectableAnimationStates.Dead);
                    colorCheckAreaManager.ColorCheckAreaStackList.Remove(colHolder.GetChild(0).gameObject);
                    colHolder.GetChild(0).transform.parent = null;
                    colorCheckAreaManager.ColorCheckAreaStackList.TrimExcess();
                }
            }

            if (_count == 0)
            {
                if (gameObject.activeInHierarchy)
                {
                    transform.DOScaleZ(0f, 0.5f).OnComplete(() => this.gameObject.SetActive(false));
                }
            }
        }

        public void CheckColorForTurrets()
        {
            bool ischeck;
            if (colorCheckAreaManager.ColorType != StackSignals.Instance.onGetColorType())
            {

                ischeck =true;

                ColorCheckAreaSignals.Instance.onTurretIsActive?.Invoke(ischeck);
            }
            else
            {
            
                ischeck=false;

                ColorCheckAreaSignals.Instance.onTurretIsActive?.Invoke(ischeck);
            }
        }

        private void SetColorMaterial()
        {
            _meshRenderer.material.color = colorCheckAreaManager.Datas[(int)colorCheckAreaManager.ColorType].Color;
        }
    }
}