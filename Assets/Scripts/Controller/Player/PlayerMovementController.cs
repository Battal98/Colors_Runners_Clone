using System;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private Rigidbody rigidbody;

        #endregion

        #region Private Variables

        [Header("Data")] private PlayerMovementData _movementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private Vector2 _inputValue;
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
                movementList.MovementTypeList[0].DoMovement(ref _isReadyToMove, rigidbody, ref _inputValue,
                    ref _movementData, ref _clampValues);
            }
            else
                Stop();
        }

        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}