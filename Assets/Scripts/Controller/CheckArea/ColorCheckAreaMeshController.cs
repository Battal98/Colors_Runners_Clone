using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;

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

        private int _stackListCount;
        private MeshRenderer _meshRenderer;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _meshRenderer = transform.parent.GetComponentInChildren<MeshRenderer>();
            _meshRenderer.material = colorCheckAreaManager.Datas[(int)colorCheckAreaManager.ColorType].Material;
        }

        public void CheckColorsForDrone()
        {
            _stackListCount = colorCheckAreaManager.ColorCheckAreaStackList.Count;
            Debug.Log(_stackListCount);

            colorCheckAreaManager.transform.GetChild(1).gameObject.SetActive(false);

            if (_stackListCount == 0)
            {
                transform.DOScaleZ(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));
                return;
            }

            for (int i = 0; i < _stackListCount; i++)
            {
                var colManager = colHolder.GetChild(0).GetComponent<CollectableManager>();
                if (colorCheckAreaManager.ColorType == colManager.CollectableColorType)
                {
                    colManager.gameObject.GetComponentInChildren<Collider>().enabled = true;
                    colorCheckAreaManager.ColorCheckAreaStackList.Remove(colManager.gameObject);
                    StackSignals.Instance.onGetStackList?.Invoke(colManager.gameObject);
                    colorCheckAreaManager.ColorCheckAreaStackList.TrimExcess();

                }
                else
                {
                    colManager.SetAnim(CollectableAnimationStates.Dead);
                    colorCheckAreaManager.ColorCheckAreaStackList.Remove(colManager.gameObject);
                    colorCheckAreaManager.ColorCheckAreaStackList.TrimExcess();
                    colHolder.GetChild(0).gameObject.transform.parent = null;
                }
            }
            transform.DOScaleZ(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));



            //for (var i = 0; i < _count; i++)
            //{
            //    var colManager = colHolder.GetChild(0).GetComponent<CollectableManager>();
            //    if (colorCheckAreaManager.ColorType == colManager.CollectableColorType)
            //    {
            //        colHolder.GetChild(0).GetComponentInChildren<Collider>().enabled = true;
            //        colorCheckAreaManager.ColorCheckAreaStackList.Remove(colHolder.GetChild(0).gameObject);
            //        StackSignals.Instance.onGetStackList?.Invoke(colHolder.GetChild(0).gameObject);
            //        colorCheckAreaManager.ColorCheckAreaStackList.TrimExcess();
            //    }
            //    else
            //    {
            //        if (gameObject.activeInHierarchy)
            //            transform.DOScaleZ(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));

            //        colManager.SetAnim(CollectableAnimationStates.Dead);
            //        colorCheckAreaManager.ColorCheckAreaStackList.Remove(colHolder.GetChild(0).gameObject);

            //        colorCheckAreaManager.ColorCheckAreaStackList.TrimExcess();
            //    }
            //}

        }

        public void CheckColorForTurrets()
        {
            if (colorCheckAreaManager.ColorType != StackSignals.Instance.onGetColorType())
                colorCheckAreaManager.SetTurretActive(true);
            else
                colorCheckAreaManager.SetTurretActive(false);
        }
    }
}