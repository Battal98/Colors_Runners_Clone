using System.Collections;
using System.Collections.Generic;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class DroneCheckCountCommand
    {
        #region Self Variables

        #region Private Variables

        private readonly List<ColorCheckPhysicController> _stackList;
        private readonly ColorCheckAreaManager _manager;

        #endregion

        #endregion

        public DroneCheckCountCommand(ref List<ColorCheckPhysicController> stackList, ref ColorCheckAreaManager manager)
        {
            _stackList = stackList;
            _manager = manager;
        }

        public IEnumerator Execute(List<GameObject> _stack,int index)
        {
            if (StackSignals.Instance.onSendStackCount() <= 0)
            {
                CameraSignals.Instance.onSetCameraTarget?.Invoke(null);
                yield return new WaitForSeconds(1); // wait for before drone movement 
                _manager.SetOutline(_stack,0);
                _manager.PlayDroneAnim();
                yield return new WaitForSeconds(7.5f / 2f); // kill wrong collectables
                _manager.SetOutline(_stack,25);
                _stackList[index].CheckColorsForDrone();
                CoreGameSignals.Instance.onExitDroneArea?.Invoke();
            }
        }
    }
}