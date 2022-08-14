using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Controllers
{

    public class DroneController : MonoBehaviour
    {

        [SerializeField]
        private List<Transform> pathList;
        [SerializeField]
        private float duration = 7.5f;
        private Vector3[] path;

        private void Start()
        {
            SetDronePath();
        }
        private void SetDronePath()
        {
            path = new Vector3[pathList.Count];
            for (int i = 0; i < pathList.Count; i++)
            {
                path[i] = pathList[i].transform.localPosition;
            }
        }

        public void DroneMove()
        {
            this.transform.DOLocalPath(path, duration, PathType.CatmullRom);
        }


    } 
}
