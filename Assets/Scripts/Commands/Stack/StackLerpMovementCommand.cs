using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;

namespace Commands
{
    public class StackLerpMovementCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stackList;
        private StackData _stackData;

        #endregion

        #endregion

        public StackLerpMovementCommand(ref List<GameObject> stackList, ref StackData stackData)
        {
            _stackList = stackList;
            _stackData = stackData;
        }

        public void Execute(ref Transform _playerTransform)
        {
            if (_stackList.Count > 0)
            {
                float directX = Mathf.Lerp(_stackList[0].transform.localPosition.x, _playerTransform.position.x,
                    _stackData.StackLerpXDelay);
                float directY = Mathf.Lerp(_stackList[0].transform.localPosition.y, _playerTransform.position.y,
                    1);
                float directZ = Mathf.Lerp(_stackList[0].transform.localPosition.z,
                    _playerTransform.position.z - _stackData.StackOffset, _stackData.StackLerpZDelay);
                _stackList[0].transform.localPosition = new Vector3(directX, directY, directZ);
                // _stackList[0].transform.rotation = Quaternion.Slerp(_stackList[0].transform.rotation, _playerTransform.rotation, _stackData.StackLerpXDelay);
                _stackList[0].transform.LookAt(_playerTransform);


                for (int i = 1; i < _stackList.Count; i++)
                {
                    Vector3 pos = _stackList[i - 1].transform.localPosition;
                    directX = Mathf.Lerp(_stackList[i].transform.localPosition.x, pos.x, _stackData.StackLerpXDelay);
                    directY = Mathf.Lerp(_stackList[i].transform.localPosition.y, pos.y, _stackData.StackLerpYDelay);
                    directZ = Mathf.Lerp(_stackList[i].transform.localPosition.z, pos.z - _stackData.StackOffset,
                        _stackData.StackLerpZDelay);
                    _stackList[i].transform.localPosition = new Vector3(directX, directY, directZ);
                    _stackList[i].transform.LookAt(_stackList[i - 1].transform);
                    // _stackList[i].transform.rotation = Quaternion.Slerp(_stackList[i].transform.rotation, _stackList[i - 1].transform.rotation, _stackData.StackLerpXDelay);
                }
            }
        }
    }
}