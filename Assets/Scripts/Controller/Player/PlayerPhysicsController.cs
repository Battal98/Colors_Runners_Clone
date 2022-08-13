using Enums;
using Signals;
using UnityEngine;
using Managers;
using DG.Tweening;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;

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
        private int value;
        [ShowInInspector]
        private List<GameObject> _stackList = new List<GameObject>();

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
                CoreGameSignals.Instance.onSetGameState?.Invoke(GameStates.Idle);
            }

            if (other.CompareTag("Collectable"))
            {
                var otherPhysic = other.gameObject.GetComponent<CollectablePhysicsController>();
                if (!otherPhysic.isTaken)
                {
                    otherPhysic.isTaken = true;
                    StackSignals.Instance.onAddInStack?.Invoke(other.gameObject.transform.parent.gameObject);
                }
            }

            if (other.CompareTag("Gate"))
            {
                var otherMR = other.gameObject.transform.parent.GetComponentInChildren<MeshRenderer>();
                _playerSkinnedMeshRenderer.material.color = otherMR.material.color;
            }

            if (other.CompareTag("CheckArea"))
            {
                var type = other.gameObject.GetComponentInParent<ColorCheckAreaManager>().areaType;
                switch (type)
                {
                    case ColorCheckAreaType.Drone:
                        _playerManager.PlayerStopForwards();
                        StackSignals.Instance.onSendStackList?.Invoke(_stackList);
                        StartCoroutine(MoveCollectables());
                        //stop player but not sideways
                        break;
                    case ColorCheckAreaType.Turret:
                        //forward speed down
                        //change animation state 
                        break;
                }
            }

            if (other.CompareTag("ColorCheck"))
            {

            }

            if (other.CompareTag("JumpArea"))
            {
                //StackSignals.Instance.onStackJumpPlatform?.Invoke();
                //_playerManager.transform.DOLocalMoveY(1f, 1f);
                //_playerManager.transform.DOLocalJump(new Vector3(_playerManager.transform.position.x, 1f, _playerManager.transform.position.z + 1f), 2f,0,1,false);
                _playerManager.transform.DOBlendableLocalMoveBy(Vector3.up * 3f,1f); // it works
            }

        }

        private void GetReferances()
        {
            _playerSkinnedMeshRenderer = playerMeshObj.GetComponentInChildren<SkinnedMeshRenderer>();
            _playerManager = this.gameObject.GetComponentInParent<PlayerManager>();
        }

        private IEnumerator MoveCollectables()
        {
            for (int i = 0; i < _stackList.Count; i++)
            {
                StackSignals.Instance.onTransportInStack?.Invoke(_stackList[i].gameObject);
                var collectableManager = _stackList[i].GetComponentInChildren<CollectableManager>();
                var randomValue = Random.Range(-0.2f, 2.5f);

                _stackList[i].transform.DOLocalMove(new Vector3(CalculateXPosCollectables(), 
                    _stackList[i].transform.position.y,
                    _playerManager.transform.position.z + randomValue), 0.35f).OnComplete(() =>
                    {

                        collectableManager.SetAnim(CollectableAnimationStates.Crouch);

                    });
                yield return new WaitForSeconds(0.35f);
            }
        }

        private int CalculateXPosCollectables()
        {
            if (_playerManager.transform.position.x < -0.5f)
            {
                value = -1;
            }
            else if (_playerManager.transform.position.x > 0.5f)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }
            return value;
        }
    }
}