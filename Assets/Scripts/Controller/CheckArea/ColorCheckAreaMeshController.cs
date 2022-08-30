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
                    colHolder.GetChild(0).gameObject.transform.parent = transform.parent;
                }
            }
            transform.DOScaleZ(0f, 0.5f).OnComplete(() => gameObject.SetActive(false));

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