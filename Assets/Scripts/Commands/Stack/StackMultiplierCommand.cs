using System.Collections.Generic;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class StackMultiplierCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stackList;
        private StackManager _manager;

        #endregion

        #endregion


        public StackMultiplierCommand(ref List<GameObject> stackList, ref StackManager manager)
        {
            _stackList = stackList;
            _manager = manager;
        }

        public void Execute()
        {
            int listCount = _stackList.Count;
            for (int i = 0; i < listCount; i++)
            {
                _manager.AddInStack(PoolSignals.Instance.onGetPoolObject(PoolType.Collectable));
            }
        }
    }
}