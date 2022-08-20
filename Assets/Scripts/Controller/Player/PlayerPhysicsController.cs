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
            _playerManager = gameObject.GetComponentInParent<PlayerManager>();
        }

        private void ChooseJobType()
        {
            switch (_checkAreaType)
            {
                case ColorCheckAreaType.Turret:
                    ColorCheckAreaSignals.Instance.onChangeJobsOnColorArea?.Invoke(ColorCheckAreaType.Turret);
                    break;
            }
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
                ChooseJobType();
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
              
            }
        }
    }
}