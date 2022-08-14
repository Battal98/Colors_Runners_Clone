using System.Collections;
using System.Collections.Generic;
using Datas.ValueObject;
using Managers;
using UnityEngine;

namespace Commands
{
    public class TransportInStack
    {
        #region Self Variables
        #region Private Variables

        private StackManager _manager;
        private List<GameObject> _stackList;
        private StackData _stackData;
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
            //_stackList.TrimExcess();
            _obj.transform.parent = target;
            if (_stackList.Count >= _stackData.StackLimit)
            {
                _stackList[_stackData.StackLimit - 1].SetActive(true);
                _manager.CollectableAnimSet(_stackList[_stackData.StackLimit - 1]);
            }
        }
    }
}