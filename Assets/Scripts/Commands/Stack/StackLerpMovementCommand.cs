using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class StackLerpMovementCommand
    {
        public void StackLerpMovement(ref List<GameObject> _stackList, Transform _playerManager, ref float _stackLerpXDelay,ref float _stackLerpYDelay,ref float offset)
        {
            if (_stackList.Count > 0)
            {
                float directX = Mathf.Lerp(_stackList[0].transform.localPosition.x, _playerManager.position.x, _stackLerpXDelay);
                float directY = Mathf.Lerp(_stackList[0].transform.localPosition.y, _playerManager.position.y, _stackLerpYDelay);
                float directZ = Mathf.Lerp(_stackList[0].transform.localPosition.z, _playerManager.position.z-offset, _stackLerpXDelay);
                _stackList[0].transform.localPosition = new Vector3(directX, directY, 0);
                _stackList[0].transform.rotation=Quaternion.Slerp(_stackList[0].transform.rotation,_playerManager.rotation,_stackLerpXDelay); 
                
                 
                for (int i = 1; i < _stackList.Count; i++)
                {
                    Vector3 pos =_stackList[i - 1].transform.localPosition ;
                    directX = Mathf.Lerp(_stackList[i].transform.localPosition.x, pos.x, _stackLerpXDelay);  
                    directY = Mathf.Lerp(_stackList[i].transform.localPosition.y, pos.y, _stackLerpYDelay);
                    directZ = Mathf.Lerp(_stackList[i].transform.localPosition.z, pos.z-offset,_stackLerpXDelay);
                    _stackList[i].transform.localPosition = new Vector3(directX, directY, directZ);
                    _stackList[i].transform.rotation=Quaternion.Slerp(_stackList[i].transform.rotation,_stackList[i-1].transform.rotation,_stackLerpXDelay); 

                 
                }
            }
            
        }

    }
}