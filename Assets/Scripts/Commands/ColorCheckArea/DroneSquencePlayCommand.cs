using System.Collections;
using System.Collections.Generic;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Commands.ColorCheckArea
{
    public class DroneSquencePlayCommand
    {
        #region Self Variables

        #region Private Variables

        private readonly MiniGameAreaManager _manager;

        #endregion

        #endregion

        public DroneSquencePlayCommand(ref MiniGameAreaManager manager)
        {
            _manager = manager;
        }

        public IEnumerator Execute()
        {
        
                CameraSignals.Instance.onSetCameraTarget?.Invoke(null);
                yield return new WaitForSeconds(1); // wait for before drone movement 
                _manager.PlayDroneAnim();
                yield return new WaitForSeconds(7.5f / 2f); // kill wrong collectables
                CoreGameSignals.Instance.onExitColorCheckArea?.Invoke(ColorCheckAreaType.Drone);
            
        }
    }
}