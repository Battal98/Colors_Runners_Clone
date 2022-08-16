using Cinemachine;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class CameraManager: MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        public CinemachineVirtualCamera RunnerCamera;
        public CinemachineVirtualCamera IdleGameCamera;
        [SerializeField]
        private CinemachineStateDrivenCamera stateDrivenCamera;
        
        #endregion

        #region Serialized Variables
        
        #endregion

        #region Private Variables
        
        [ShowInInspector] private Vector3 _initialPosition;
        private CinemachineTransposer _miniGameTransposer;
        private  Animator _animator;
        private CameraStatesType _cameraStatesType = CameraStatesType.Runner;
        private Transform _playerManager;

        #endregion

        #endregion

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _miniGameTransposer = IdleGameCamera.GetCinemachineComponent<CinemachineTransposer>();
            GetInitialPosition();
        }

        #region Event Subscriptions
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += SetCameraTarget;
            CameraSignals.Instance.onSetCameraState += OnSetCameraState;
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= SetCameraTarget;
            CameraSignals.Instance.onSetCameraState -= OnSetCameraState;
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        private void GetInitialPosition()
        {
            _initialPosition = transform.localPosition;
        }
        private void OnMoveToInitialPosition()
        {
            transform.localPosition = _initialPosition;
        }

        private void SetCameraTarget()
        {
            _playerManager = FindObjectOfType<PlayerManager>().transform;
            CameraSignals.Instance.onSetCameraTarget?.Invoke(_playerManager);
        }

        private void OnSetCameraTarget(Transform _target)
        {
            stateDrivenCamera.Follow = _target;
            stateDrivenCamera.Follow = _target;
        }

        private void OnReset()
        {
            stateDrivenCamera.Follow = null;
            stateDrivenCamera.LookAt = null;
            OnMoveToInitialPosition();
        }

        public void OnSetCameraState(CameraStatesType cameraState)
        {
            
                _animator.SetTrigger(cameraState.ToString());
         
        }
    }
}