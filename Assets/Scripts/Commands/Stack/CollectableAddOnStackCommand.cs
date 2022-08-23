using System.Collections.Generic;
using Datas.ValueObject;
using Managers;
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
            

            //bunun olmamasi gerekiyor. Color Check kisminda arkadaki gorunmez olanlar stackte kaliyor.
            if (_stackList.Count > _stackData.StackLimit)
            {
                _obj.SetActive(false);
            }
        }
    }
}