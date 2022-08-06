using UnityEngine;
using Signals;
using Keys;
using Enums;


namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private GameStates _gameStates = GameStates.RunnerGameState;

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
            _gameStates = gameStates;
            CoreGameSignals.Instance.onGetGameState?.Invoke(_gameStates);
        }

    }
}