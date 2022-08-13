using System;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables

        #endregion
    
        #region Serialized Variables

        #endregion
    
        #region Private Variables

        private DOTweenAnimation _obstacleAnimation;

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {

            CoreGameSignals.Instance.onPlay += OnObstacleAnimationStart;
        }

        private void Unsubscribe()
        {
            
            CoreGameSignals.Instance.onPlay -= OnObstacleAnimationStart;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void Awake()
        {
            _obstacleAnimation = this.GetComponent<DOTweenAnimation>();
        }

        private void OnObstacleAnimationStart()
        {
            _obstacleAnimation.DOPlay();
        }
    }
}