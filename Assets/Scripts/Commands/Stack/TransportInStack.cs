using System.Collections.Generic;
using Datas.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class TransportInStack
    {
        #region Self Variables

        #region Private Variables

        private readonly StackManager _manager;
        private readonly List<GameObject> _stackList;
        private readonly StackData _stackData;

        #endregion

        #endregion

        public TransportInStack(ref List<GameObject> stackList, ref StackManager manager, ref StackData stackdata)
        {
            _stackList = stackList;
            _manager = manager;
            _stackData = stackdata;
        }


        public void Execute(GameObject _obj, Transform target)
        {
            _stackList.Remove(_obj);
            _obj.transform.parent = target;
            if (_stackList.Count >= _stackData.StackLimit)
            {
                _stackList[_stackData.StackLimit - 1].SetActive(true);
                _manager.CollectableAnimSet(_stackList[_stackData.StackLimit - 1], CollectableAnimationStates.Run);
            }

            if (_stackList.Count == 0) StackSignals.Instance.onStackTransferComplete?.Invoke();

            _stackList.TrimExcess();
        }
    }
}