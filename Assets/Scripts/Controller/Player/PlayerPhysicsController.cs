using Enums;
using Signals;
using UnityEngine;
using Managers;
using DG.Tweening;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;
using MK.Toon;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private GameObject playerMeshObj;

        #endregion

        #region Private Variables

        private SkinnedMeshRenderer _playerSkinnedMeshRenderer;
        private PlayerManager _playerManager;
        private int _value;
        private int _count;
        private Transform _target;
        [ShowInInspector]
        private List<GameObject> _stackList = new List<GameObject>();
        private ColorCheckAreaType type;
        #endregion

        #endregion

        private void Awake()
        {
            GetReferances();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                Debug.Log("girdik");
                CoreGameSignals.Instance.onSetGameState?.Invoke(GameStates.Idle);
            }

        

            if (other.CompareTag("Gate"))
            {
                var otherMR = other.gameObject.transform.parent.GetComponentInChildren<MeshRenderer>();
                _playerSkinnedMeshRenderer.material.color = otherMR.material.color;
            }
            
            if (other.CompareTag("JumpArea"))
            {
                _playerManager.transform.DOBlendableLocalMoveBy(Vector3.up * 3f,1f); // it works
            }

            if (other.CompareTag("CheckArea"))
            {
                Debug.Log(other.transform.parent);
                ColorCheckAreaSignals.Instance.onCheckAreaControl?.Invoke(other.transform.parent.gameObject);
            }

        }

        private void GetReferances()
        {
            _playerSkinnedMeshRenderer = playerMeshObj.GetComponentInChildren<SkinnedMeshRenderer>();
            _playerManager = this.gameObject.GetComponentInParent<PlayerManager>();
        }
    }
}