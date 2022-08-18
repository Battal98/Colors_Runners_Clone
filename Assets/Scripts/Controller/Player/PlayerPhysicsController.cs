using System;
using Enums;
using Signals;
using UnityEngine;
using Managers;
using DG.Tweening;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;
using MK.Toon;

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
                Debug.Log("girdk");
                _checkAreaType = other.GetComponentInParent<MiniGameAreaManager>().AreaType;
              
                ColorCheckAreaSignals.Instance.onCheckAreaControl?.Invoke(other.transform.parent.gameObject);
            
               
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("CheckArea"))
            {
                Debug.Log("Çıktık");
                if (_checkAreaType==ColorCheckAreaType.Turret)
                {
                    _playerManager.ExitColorCheckArea(ColorCheckAreaType.Turret);
                }
              
            
            }
        }
    }
}