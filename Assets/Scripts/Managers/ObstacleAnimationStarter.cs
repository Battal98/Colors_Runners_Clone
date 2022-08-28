using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ObstacleAnimationStarter : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private DOTweenAnimation _obstacleAnimation;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _obstacleAnimation = this.GetComponent<DOTweenAnimation>();
        }

        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void Unsubscribe()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion


        private void OnPlay()
        {
            _obstacleAnimation.DOPlay();
        }
    }
}