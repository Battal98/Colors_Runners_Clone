using System.Collections.Generic;
using Enums;
using Managers;
using Signals;
using UnityEngine;

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

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _meshRenderer = this.transform.parent.GetComponentInChildren<MeshRenderer>();
        }

        public void CheckColorsForDrone(List<GameObject> ColorCheckAreaStackList, Transform colHolder)
        {
            _count = ColorCheckAreaStackList.Count;
            transform.GetComponent<Collider>().enabled = false;

            for (int i = 0; i < _count; i++)
            {
                var color = colHolder.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.color;
                if (ColorUtility.ToHtmlStringRGB(_meshRenderer.material.color) != ColorUtility.ToHtmlStringRGB(color))
                {
                    colHolder.GetChild(0).gameObject.GetComponent<CollectableManager>()
                        .SetAnim(CollectableAnimationStates.Dead);
                    ColorCheckAreaStackList.Remove(colHolder.GetChild(0).gameObject);
                    colHolder.GetChild(0).transform.parent = null;
                    ColorCheckAreaStackList.TrimExcess();
                }
                else
                {
                    colHolder.GetChild(0).GetComponentInChildren<Collider>().enabled = true;
                    ColorCheckAreaStackList.Remove(colHolder.GetChild(0).gameObject);
                    StackSignals.Instance.onGetStackList?.Invoke(colHolder.GetChild(0).gameObject);
                    ColorCheckAreaStackList.TrimExcess();
                }
            }
        }

        public void CheckColorForTurrets(TurretController turretControllers, Transform target,
            List<GameObject> ColorCheckAreaStackList)
        {
            _count = ColorCheckAreaStackList.Count;
            transform.GetComponent<Collider>().enabled = false;

            for (int i = 0; i < _count; i++)
            {
                var color = colHolder.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.color;
                if (ColorUtility.ToHtmlStringRGB(_meshRenderer.material.color) != ColorUtility.ToHtmlStringRGB(color))
                {
                    turretControllers.targetPlayer = target.transform;
                    turretControllers.isTargetPlayer = true;
                }
                else
                {
                    turretControllers.targetPlayer = null;
                    turretControllers.isTargetPlayer = false;
                }
            }
        }
    }
}