using System.Collections;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
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
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke(null);
            yield return new WaitForSeconds(1); // wait for before drone movement 
            _manager.SetCollectableOutline(0);
            _manager.PlayDroneAnim();
            yield return new WaitForSeconds(7.5f / 2f); // kill wrong collectables
            _manager.SetCollectableOutline(25);
            _manager.ChangeJobType(ColorCheckAreaType.Drone);
            CoreGameSignals.Instance.onExitColorCheckArea?.Invoke(ColorCheckAreaType.Drone);
        }
    }
}