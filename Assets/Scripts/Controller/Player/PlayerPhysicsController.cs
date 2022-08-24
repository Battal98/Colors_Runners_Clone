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

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish")) CoreGameSignals.Instance.onSetGameState?.Invoke(GameStates.Idle);

            if (other.CompareTag("CheckArea"))
            {
                _checkAreaType = other.GetComponentInParent<MiniGameAreaManager>().AreaType;

                ColorCheckAreaSignals.Instance.onCheckAreaControl?.Invoke(other.transform.parent.gameObject);
                playerManager.ChangeSpeed(_checkAreaType);
                playerManager.ChangeScoreAreaVisible(_checkAreaType);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CheckArea"))
            {
                playerManager.ExitColorCheckArea(_checkAreaType);
                playerManager.ChangeScoreAreaVisible(_checkAreaType);
            }
        }
    }
}