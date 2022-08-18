using Enums;
using Signals;
using UnityEngine;
using Managers;


namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Private Variables

        private PlayerManager _playerManager;
        private ColorCheckAreaType _checkAreaType;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferances();
        }

        private void GetReferances()
        {
            _playerManager = this.gameObject.GetComponentInParent<PlayerManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                CoreGameSignals.Instance.onSetGameState?.Invoke(GameStates.Idle);
            }

            if (other.CompareTag("CheckArea"))
            {
                _checkAreaType = other.GetComponentInParent<MiniGameAreaManager>().AreaType;

                ColorCheckAreaSignals.Instance.onCheckAreaControl?.Invoke(other.transform.parent.gameObject);
                _playerManager.ChangeSpeed(_checkAreaType);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CheckArea"))
            {
                if (_checkAreaType == ColorCheckAreaType.Turret)
                {
                    _playerManager.ExitColorCheckArea(ColorCheckAreaType.Turret);
                }
                else if (_checkAreaType == ColorCheckAreaType.Drone)
                {
                    _playerManager.ExitColorCheckArea(ColorCheckAreaType.Drone);
                }
            }
        }
    }
}