using DG.Tweening;
using Signals;
using UnityEngine;
namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody rigidbody;
        #endregion
        #endregion

        private void OnTriggerEnter(Collider other)
        {
        }
    }
}