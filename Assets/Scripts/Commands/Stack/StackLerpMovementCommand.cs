using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class StackLerpMovementCommand
    {
        public void StackLerpMovement(ref List<GameObject> _stackList, Transform _playerManager, ref float _stackLerpDelay)
        {
             if (_stackList.Count > 0)
             {
                 _stackList[0].transform.position = Vector3.Lerp(_stackList[0].transform.position,
                         new Vector3(_playerManager.position.x, _playerManager.position.y,
                            _playerManager.position.z + _playerManager.localScale.z * -0.3f),
                         _stackLerpDelay);

                for (int i = 1; i < _stackList.Count; i++)
                 {
                     _stackList[i].transform.position = Vector3.Lerp(_stackList[i].transform.position,
                         new Vector3(_stackList[i - 1].transform.position.x, _stackList[i - 1].transform.position.y,
                             _stackList[i - 1].transform.position.z + _playerManager.localScale.z * -0.15f),
                         _stackLerpDelay );
                 }
             }
            
        }

    }
}