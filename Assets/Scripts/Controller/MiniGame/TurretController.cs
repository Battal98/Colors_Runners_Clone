using System;
using UnityEngine;
using System.Collections;
using RootMotion;
using RootMotion.FinalIK;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class TurretController : MonoBehaviour
    {
            #region self Variables

            #region Serialized Variables

            [SerializeField] private Transform targetPlayer;
            [SerializeField] private Transform targetRandom;

            #endregion

            #region Public Variables

            public Part[] parts;
            public bool isTargetPlayer;
            public GameObject Bullet;
            public GameObject BulletSpawnPoint;
            public float ShootTimer = 1f;
            
            #endregion

            #region Private Variables
            private bool shootReady = true;
            

            #endregion
            #endregion
      

        private void Start()
        {
            StartCoroutine(SetTarget());
        }

        private IEnumerator SetTarget()
        {
            while (true)
            {
                float xValue = Random.Range(-1f, 1f);
                float yValue = Random.Range(0.5f, 1f);
                float zValue = Random.Range(-1f, 1f);
                targetRandom.DOLocalMove(new Vector3(xValue, yValue, zValue), 1f);
                yield return new WaitForSeconds(1.1f);
            }
        }

        public void SetTarget(Transform target)
        {
            targetPlayer = target;
        }
        
        void Update()
        {
            if (isTargetPlayer)
            {
                foreach (Part part in parts) part.AimAt(targetPlayer);
                
                if (shootReady)
                {
                    Shoot();
                }
                return;
            }
            foreach (Part part in parts) part.AimAt(targetRandom);
        }

        public void Shoot()
        {
           Transform _bullet = Instantiate(Bullet.transform,BulletSpawnPoint.transform.position, Quaternion.identity);
           _bullet.transform.rotation = BulletSpawnPoint.transform.rotation;
           shootReady = false;
           StartCoroutine(ShootRate());
          
        }

        private IEnumerator ShootRate()
        {
            yield return new WaitForSeconds(ShootTimer);
            shootReady = true;
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