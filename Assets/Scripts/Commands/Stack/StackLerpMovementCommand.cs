using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class StackLerpMovementCommand
    {
        public void StackLerpMovement(ref List<GameObject> _stackList, Transform _playerManager, ref float _stackLerpDelay,ref float offset)
        {
             if (_stackList.Count > 0)
             {
                 float directX = Mathf.Lerp(_stackList[0].transform.localPosition.x, _playerManager.position.x, _stackLerpDelay);
                 float directY = Mathf.Lerp(_stackList[0].transform.localPosition.y, _playerManager.position.y, _stackLerpDelay);
                 float directZ = Mathf.Lerp(_stackList[0].transform.localPosition.z, _playerManager.position.z-offset, 1);
                 _stackList[0].transform.localPosition = new Vector3(directX, directY, directZ);
                 
                for (int i = 1; i < _stackList.Count; i++)
                 {
                     Vector3 pos =_stackList[i - 1].transform.localPosition ;
                      directX = Mathf.Lerp(_stackList[i].transform.localPosition.x, pos.x, _stackLerpDelay);
                      directY = Mathf.Lerp(_stackList[i].transform.localPosition.y, pos.y, _stackLerpDelay);
                      directZ = Mathf.Lerp(_stackList[i].transform.localPosition.z, pos.z-offset, 1);
                     _stackList[i].transform.localPosition = new Vector3(directX, directY, directZ);
                 }
             }
            
        }

    }
}