using System;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Managers;
using UnityEngine;
using DG.Tweening;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private Rigidbody rbody;
        [SerializeField] private CharacterController characterController;
        [SerializeField] bool isJoystick = false;

        #endregion

        #region Private Variables

        [Header("Data")] private PlayerMovementData _movementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private Vector2 _inputValue;
        private Vector3 _inputValue2;
        private Vector2 _clampValues;
        private CD_MovementList movementList;

        #endregion

        #endregion

        private void Awake()
        {
            movementList = GetMovementTypeList();
        }

        private CD_MovementList GetMovementTypeList() => Resources.Load<CD_MovementList>("Data/CD_MovementList");

        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            _movementData = dataMovementData;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }

        public void UpdateInputValue(InputParams inputParam)
        {
            _inputValue = inputParam.Values;
            _inputValue2 = inputParam.Values;
            _clampValues = inputParam.ClampValues;
        }

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (isJoystick)
                {
                    movementList.MovementTypeList[1].DoJoystickMovement(ref _isReadyToMove, rbody, ref _inputValue2,
                        ref _movementData, ref  characterController, this.gameObject.transform);
                }
                else
                {
                    movementList.MovementTypeList[0].DoSwerveMovement(ref _isReadyToMove, rbody, ref _inputValue,
                        ref _movementData, ref _clampValues);
                }

            }
            else
                Stop();

        }

        private void Stop()
        {
            rbody.velocity = Vector3.zero;
            rbody.angularVelocity = Vector3.zero;
        }

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}