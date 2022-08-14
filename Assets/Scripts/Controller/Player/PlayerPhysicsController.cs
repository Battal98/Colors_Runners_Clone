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
                        StackSignals.Instance.onSetStackList?.Invoke(_stackList);
                        _playerManager.PlayerChangeForwardSpeed(0);
                        StartCoroutine(MoveCollectables(other.gameObject));
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
                 _target = other.transform.parent.transform.GetChild(2);
            }

            if (other.CompareTag("JumpArea"))
            {
                _playerManager.transform.DOBlendableLocalMoveBy(Vector3.up * 3f,1f); // it works
            }

        }

        private void GetReferances()
        {
            _playerSkinnedMeshRenderer = playerMeshObj.GetComponentInChildren<SkinnedMeshRenderer>();
            _playerManager = this.gameObject.GetComponentInParent<PlayerManager>();
        }

        #region Collectable Movement Color Check Area
        private IEnumerator MoveCollectables(GameObject other)
        {
            var colorCheckAreaManager =  other.transform.GetComponentInParent<ColorCheckAreaManager>(); 
            for (int i = 0; i < _stackList.Count; i++)
            {
                if (!_target)
                {
                    break;
                }
                StackSignals.Instance.onTransportInStack?.Invoke(_stackList[i].gameObject, _target);
                var collectableManager = _stackList[i].GetComponentInChildren<CollectableManager>();
                var randomValue = Random.Range(0.2f, 2.8f);
                _count++;
                _stackList[i].transform.DOMove(new Vector3(CalculateXPosCollectables(),
                    _stackList[i].transform.position.y,
                    _playerManager.transform.position.z + randomValue), 1f).OnComplete(() =>
                    {

                        collectableManager.SetAnim(CollectableAnimationStates.Crouch);

                    });
                _stackList[i].transform.DORotate(Vector3.zero, 0.1f);

                StartCoroutine(CheckCount(colorCheckAreaManager));
                yield return new WaitForSeconds(0.15f);
            }
        }

        private int CalculateXPosCollectables()
        {
            if (_playerManager.transform.position.x < -0.5f)
            {
                _value = -1;
            }
            else if (_playerManager.transform.position.x > 0.5f)
            {
                _value = 1;
            }
            else
            {
                _value = 0;
            }
            return _value;
        } 

        private IEnumerator CheckCount(ColorCheckAreaManager otherColorCheckAreaManager) 
        {
            if (_count >= _stackList.Count)
            {
                #region Collectable Material Outline Jobs

                OutlineJobs(0);

                #endregion

                #region Drone Movement and Color Check Jobs

                CameraSignals.Instance.onSetCameraTarget?.Invoke(null);
                yield return new WaitForSeconds(.5f); // wait for before drone movement 
                otherColorCheckAreaManager.PlayDroneAnim();
                yield return new WaitForSeconds(7.5f / 2f);// kill wrong collectables
                Debug.Log("girdi");
                otherColorCheckAreaManager.CheckColor();
                yield return new WaitForSeconds(7.5f / 2f);// wait for drone movement
                AfterDroneMovementJobs(); 
                #endregion
            }
        }

        private void CheckColor()
        {

        }

        private void OutlineJobs(float endValue)
        {
            for (int i = 0; i < _stackList.Count; i++)
            {
                var materialColor = _stackList[i].GetComponentInChildren<SkinnedMeshRenderer>().material;
                materialColor.DOFloat(endValue, "_OutlineSize", 0.5f);
            }

        }

        private void AfterDroneMovementJobs()
        {
            StackSignals.Instance.onGetStackList?.Invoke(_stackList);
            CameraSignals.Instance.onSetCameraTarget?.Invoke(_playerManager.transform);
            _playerManager.transform.DOMoveZ(_playerManager.transform.position.z + 2.9f, .5f);
            _playerManager.PlayerChangeForwardSpeed(1);
            OutlineJobs(15);
        }
        #endregion
    }
}