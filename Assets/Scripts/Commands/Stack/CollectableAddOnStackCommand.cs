using System.Collections.Generic;
using Datas.ValueObject;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class CollectableAddOnStackCommand
    {
        #region Self Variables

        #region Private Variables

        private readonly StackManager _stackManager;
        private readonly List<GameObject> _stackList;
        private readonly StackData _stackData;

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

            if (_stackList.Count > _stackData.StackLimit) _obj.SetActive(false);

            _stackList.Add(_obj);
            var pivot = _stackList[_stackList.Count - 1].transform.position;

            _obj.transform.localPosition =
                new Vector3(pivot.x, pivot.y, pivot.z - _stackData.StackOffset * _stackList.Count * 2);
            ScoreSignals.Instance.onGetPlayerScore?.Invoke(_stackList.Count);
        }
    }
}