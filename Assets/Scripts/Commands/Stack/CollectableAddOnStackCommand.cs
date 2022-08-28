using System.Collections.Generic;
using Datas.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class CollectableAddOnStackCommand
    {
        #region Self Variables

        #region Private Variables

        private StackManager _stackManager;
        private List<GameObject> _stackList;
        private StackData _stackData;

        #endregion

        #endregion

        public CollectableAddOnStackCommand(ref StackManager stackManager, ref List<GameObject> stackList,
            ref StackData stackData)
        {
            _stackList = stackList;
            _stackManager = stackManager;
            _stackData = stackData;
        }

        public void Execute(GameObject _obj)
        {
            _obj.transform.parent = _stackManager.transform;
            _stackList.Add(_obj);
            Vector3 pivot = _stackList[_stackList.Count - 1].transform.position;
            _obj.transform.localPosition = new Vector3(pivot.x,pivot.y,pivot.z- ((_stackData.StackOffset)*_stackList.Count*2));
            if (_stackList.Count > _stackData.StackLimit)
            {
               _obj.SetActive(false);
            }

            ScoreSignals.Instance.onGetScore?.Invoke(_stackList.Count);
        }
    }
}