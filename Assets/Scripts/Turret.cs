using UnityEngine;
using System.Collections;
using RootMotion;
using RootMotion.FinalIK;
using DG.Tweening;

namespace RootMotion.Demos {

	/// <summary>
	/// Demo for aiming a Turret. All it does is call Transform.LookAt on each part and apply rotation limits one by one, starting from the parent.
	/// </summary>
	public class Turret : MonoBehaviour {

		/// <summary>
		/// An independent part of the turret
		/// </summary>
		[System.Serializable]
		public class Part {

			public Transform transform; // The Transform
			private RotationLimit rotationLimit; // The Rotation Limit component

			// Aim this part at the target
			public void AimAt(Transform target) {
				transform.LookAt(new Vector3(target.position.x, target.position.y+0.5f, target.position.z), transform.up);

				// Finding the Rotation Limit
				if (rotationLimit == null) {
					rotationLimit = transform.GetComponent<RotationLimit>();
					rotationLimit.Disable();
				}
				// Apply rotation limits
				rotationLimit.Apply();
			}
		}
        public Transform targetPlayer; // The aiming target
        public Transform targetRandom; // The aiming target
		public Part[] parts; // All the turret parts

        private void Awake()
        {
			targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
			targetRandom = GameObject.FindGameObjectWithTag("Target").transform;
		}
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
				targetRandom.DOLocalMove(new Vector3(xValue, yValue, zValue),1f);
				yield return new WaitForSeconds(1.1f);
			}
        }

		public bool isTargetPlayer;
		void Update() {

			if (isTargetPlayer)
            {
				foreach (Part part in parts) part.AimAt(targetPlayer);
				return;
			}
			foreach (Part part in parts) part.AimAt(targetRandom);

		}

		private void ShootPlayer()
        {

        }
	}
}
