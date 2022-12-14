using UnityEngine;
using Signals;
using Enums;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private CameraStatesType _cameraType = CameraStatesType.Idle;

        #endregion

        #endregion

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
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onSetGameState -= OnSetGameState;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSetGameState(GameStates gameStates)
        {
            CoreGameSignals.Instance.onGetGameState?.Invoke(gameStates);
        }

        private void OnReset()
        {
            CoreGameSignals.Instance.onGetGameState?.Invoke(GameStates.Runner);
        }
    }
}