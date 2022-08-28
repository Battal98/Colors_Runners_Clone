using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class DroneController : MonoBehaviour
    {
        #region Private Variables

        private Vector3[] _path;

        #endregion

        #region Serialized Variables

        [SerializeField] private List<Transform> pathList;
        [SerializeField] private float duration = 7.5f;

        #endregion


        private void Start()
        {
            SetDronePath();
        }

        private void SetDronePath()
        {
            _path = new Vector3[pathList.Count];
            for (int i = 0; i < pathList.Count; i++)
            {
                _path[i] = pathList[i].transform.localPosition;
            }
        }

        public void DroneMove()
        {
            this.transform.DOLocalPath(_path, duration, PathType.CatmullRom);
        }
    }
}