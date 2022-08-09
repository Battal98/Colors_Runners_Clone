using System;
using UnityEngine;
using Signals;
using Keys;
using Enums;
using Sirenix.OdinInspector;


namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameStates gameStates = GameStates.Runner;

        private CameraStatesType cameraType = CameraStatesType.Idle;

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onSetGameState += OnSetGameState;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onSetGameState -= OnSetGameState;
            

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSetGameState(GameStates gameStates)
        {
            CoreGameSignals.Instance.onGetGameState?.Invoke(gameStates);
            
            CameraSignals.Instance.onSetCameraState?.Invoke(cameraType);
        }
    }
}