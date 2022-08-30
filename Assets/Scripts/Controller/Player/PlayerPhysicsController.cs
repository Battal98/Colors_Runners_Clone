using System;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region Private Variables

        private ColorCheckAreaType _checkAreaType;
        private int _timer;

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                CoreGameSignals.Instance.onSetGameState?.Invoke(GameStates.Idle);
                StackSignals.Instance.onEnterFinish?.Invoke();
            }

            if (other.CompareTag("CheckArea"))
            {
                _checkAreaType = other.GetComponentInParent<MiniGameAreaManager>().AreaType;

                CoreGameSignals.Instance.onCheckAreaControl?.Invoke(other.transform.parent.gameObject);
                playerManager.ChangeSpeed(_checkAreaType);
                playerManager.ChangeScoreAreaVisible(_checkAreaType);
            }

            if (other.CompareTag("BuildArea"))
            {
                IdleGameSignals.Instance.onCheckArea?.Invoke(other.transform.parent.gameObject);
            }

            if (other.CompareTag("Multiplier"))
            {
                StackSignals.Instance.onEnterMultiplier?.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("BuildArea"))
            {
                if (_timer >= 20)
                {
                    StackSignals.Instance.onCollectablesThrow?.Invoke(transform.parent);
                    playerManager.DownCost();
                    _timer = _timer * 60 / 100;
                }

                else
                {
                    _timer++;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CheckArea"))
            {
                playerManager.ExitColorCheckArea(_checkAreaType);
                playerManager.ChangeScoreAreaVisible(_checkAreaType);
            }


            if (other.CompareTag("BuildArea"))
            {
                _timer = 0;
            }
        }
    }
}