using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RootMotion;
using RootMotion.FinalIK;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEditor.PackageManager;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class TurretController : MonoBehaviour
    {
        #region self Variables

        #region Public Variables

        public bool isTargetPlayer;

        #endregion

        #region Serialized Variables
        
       
        [SerializeField] private Part[] parts;
        [SerializeField] private Transform targetPlayer;
        [SerializeField] private Transform targetRandom;
        [SerializeField] private List<ParticleSystem> _particlepart;

        #endregion

        #region Private Variables

        private bool shootReady = true;
        

        #endregion

        #endregion


        private void Start()
        {
            InvokeRepeating("SetTarget", 0.75f, 1);
        }

        private void SetTarget()
        {
            if (isTargetPlayer)
            {
                Shoot();
            }
            else
            {
                float xValue = Random.Range(-1.5f, 1.5f);
                float yValue = Random.Range(-0.5f, 0.5f);
                float zValue = Random.Range(0f, 2f);
                targetRandom.DOLocalMove(new Vector3(xValue, yValue, zValue), 1f);
            }
        }

        public void SetTarget(Transform target)
        {
            targetPlayer = target;
        }

        private void Update()
        {
            if (isTargetPlayer)
            {
                foreach (Part part in parts) part.AimAt(targetPlayer);
                return;
            }

            foreach (Part part in parts) part.AimAt(targetRandom);
        }   

        private void Shoot()
        {
            //partical koy
            StackSignals.Instance.onKillRandomInStack?.Invoke();
            _particlepart[0].Play();
            _particlepart[1].Play();

        }
    }

    #region Part

    [Serializable]
    public class Part
    {
        public Transform transform;
        private RotationLimit rotationLimit;


        public void AimAt(Transform target)
        {
            transform.LookAt(new Vector3(target.position.x, target.position.y + 0.3f, target.position.z),
                transform.up);


            if (rotationLimit == null)
            {
                rotationLimit = transform.GetComponent<RotationLimit>();
                rotationLimit.Disable();
            }


            rotationLimit.Apply();
        }
    }

    #endregion
}