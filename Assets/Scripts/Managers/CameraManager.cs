using Cinemachine;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        [ShowInInspector] private Vector3 _initialPosition;
        private Animator _animator;
        private CameraStatesType _cameraStatesType = CameraStatesType.Runner;
        private Transform _playerManager;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            GetInitialPosition();
        }

        private void GetReferences()
        {
            _animator = GetComponent<Animator>();
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGetGameState += OnGetGameState;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGetGameState += OnGetGameState;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void GetInitialPosition()
        {
            _initialPosition = transform.GetChild(0).localPosition;
        }

        private void MoveToInitialPosition()
        {
            transform.GetChild(0).localPosition = _initialPosition;
        }

        private void SetPlayerFollow()
        {
            _playerManager = FindObjectOfType<PlayerManager>().transform;
            OnSetCameraTarget(_playerManager);
        }

        private void OnSetCameraTarget(Transform _target)
        {
            stateDrivenCamera.Follow = _target;
        }

        private void SetCameraState(CameraStatesType cameraState)
        {
            _animator.SetTrigger(cameraState.ToString());
        }

        private void OnGetGameState(GameStates states)
        {
            switch (states)
            {
                case GameStates.Idle:
                    SetCameraState(CameraStatesType.Idle);
                    break;
                case GameStates.Runner:
                    SetCameraState(CameraStatesType.Runner);
                    break;
            }
        }

        private void OnPlay()
        {
            SetPlayerFollow();
            GetInitialPosition();
        }

        private void OnReset()
        {
            stateDrivenCamera.Follow = null;
            stateDrivenCamera.LookAt = null;
            MoveToInitialPosition();
        }
    }
}