using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Commands
{
    public class CollectableAddOnStackCommand
    {
        #region Self Variables

        #region Private Variables

        private StackManager _stackManager;
        private List<GameObject> _stacklist;

        #endregion

        #endregion

        public CollectableAddOnStackCommand(ref StackManager stackManager, ref List<GameObject> stackList)
        {
            _stacklist = stackList;
            _stackManager = stackManager;
        }

        public void Execute(GameObject _obj)
        {
            _obj.transform.parent = _stackManager.transform;
            _stacklist.Add(_obj);
        }
    }
}