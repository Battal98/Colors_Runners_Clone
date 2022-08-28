using System;
using System.Collections.Generic;
using DG.Tweening;
using RootMotion.FinalIK;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class TurretController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool IsTargetPlayer;

        #endregion

        #region Serialized Variables

        [SerializeField] private Part[] parts;
        [SerializeField] private Transform targetPlayer;
        [SerializeField] private Transform targetRandom;
        [SerializeField] private List<ParticleSystem> particlepart;

        #endregion

        #region Private Variables

        private bool _shootReady = true;

        #endregion

        #endregion


        private void Start()
        {
            InvokeRepeating("SetTarget", 0.75f, 1);
        }

        private void SetTarget()
        {
            if (IsTargetPlayer)
            {
                Shoot();
            }
            else
            {
                var xValue = Random.Range(-1.5f, 1.5f);
                var yValue = Random.Range(-0.5f, 0.5f);
                var zValue = Random.Range(0f, 2f);
                targetRandom.DOLocalMove(new Vector3(xValue, yValue, zValue), 1f);
            }
        }

        public void SetTarget(Transform target)
        {
            targetPlayer = target;
        }

        private void Update()
        {
            if (IsTargetPlayer)
            {
                foreach (var part in parts) part.AimAt(targetPlayer);
                return;
            }

            foreach (var part in parts) part.AimAt(targetRandom);
        }

        private void Shoot()
        {
            //partical koy
            StackSignals.Instance.onKillRandomInStack?.Invoke();
            for (int i = 0; i < particlepart.Count; i++)
            {
                particlepart[i].Play();
            }
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