using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;

namespace Commands
{
    public class StackLerpMovementCommand
    {
        #region Self Variables

        #region Private Variables

        private readonly List<GameObject> _stackList;
        private readonly StackData _stackData;

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
                var directX = Mathf.Lerp(_stackList[0].transform.localPosition.x, _playerTransform.position.x,
                    _stackData.StackLerpXDelay);
                var directY = Mathf.Lerp(_stackList[0].transform.localPosition.y, _playerTransform.position.y, 1);
                var directZ = Mathf.Lerp(_stackList[0].transform.localPosition.z,
                    _playerTransform.position.z - _stackData.StackOffset, _stackData.StackLerpZDelay);
                _stackList[0].transform.localPosition = new Vector3(directX, directY, directZ);
                
                _stackList[0]
                    .transform.LookAt(new Vector3(_playerTransform.transform.position.x,
                        _stackList[0]
                            .transform.position.y,
                        _playerTransform.transform.position.z));
                
                for (var i = 1; i < _stackList.Count; i++)
                {
                    var pos = _stackList[i - 1].transform.localPosition;
                    directX = Mathf.Lerp(_stackList[i].transform.localPosition.x, pos.x, _stackData.StackLerpXDelay);
                    directY = Mathf.Lerp(_stackList[i].transform.localPosition.y, pos.y, _stackData.StackLerpYDelay);
                    directZ = Mathf.Lerp(_stackList[i].transform.localPosition.z, pos.z - _stackData.StackOffset,
                        _stackData.StackLerpZDelay);
                    _stackList[i].transform.localPosition = new Vector3(directX, directY, directZ);
                    _stackList[i].transform.LookAt(new Vector3(pos.x, _stackList[i].transform.position.y, pos.z));
                }
            }
        }
    }
}