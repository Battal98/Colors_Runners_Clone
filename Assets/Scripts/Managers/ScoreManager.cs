using Signals;
using UnityEngine;
using UnityEngine.Rendering;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _playerScore;

        #endregion

        #endregion


        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            ScoreSignals.Instance.onGetPlayerScore += OnGetStackScore;

            CoreGameSignals.Instance.onEnterFinish += OnEnterFinish;
        }

        private void UnSubscribe()
        {
            ScoreSignals.Instance.onGetPlayerScore -= OnGetStackScore;


            CoreGameSignals.Instance.onEnterFinish -= OnEnterFinish;
        }


        private void OnDisable()
        {
            UnSubscribe();
        }

        #endregion


        private void OnGetStackScore(int Value)
        {
            _playerScore = Value;
            ScoreSignals.Instance.onSetPlayerScore?.Invoke(Value);
        }

        private void OnEnterFinish()
        {
            _playerScore = ScoreSignals.Instance.onGetIdleScore() + _playerScore;
            ScoreSignals.Instance.onSetIdleScore?.Invoke(_playerScore);
            ScoreSignals.Instance.onSetPlayerScore?.Invoke(_playerScore);

        }
    }
}