using Signals;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _playerCount;

        #endregion

        #endregion


        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            ScoreSignals.Instance.onGetScore += OnGetStackScore;
        }

        private void UnSubscribe()
        {
            ScoreSignals.Instance.onGetScore -= OnGetStackScore;
        }


        private void OnDisable()
        {
            UnSubscribe();
        }

        #endregion


        private void OnGetStackScore(int Value)
        {
            ScoreSignals.Instance.onSetScore?.Invoke(Value);
        }
    }
}