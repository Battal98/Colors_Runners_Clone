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

        private readonly List<GameObject> _stackList;
        private readonly StackManager _manager;

        #endregion

        #endregion

        public StackMultiplierCommand(ref List<GameObject> stackList, ref StackManager manager)
        {
            _stackList = stackList;
            _manager = manager;
        }

        public void Execute()
        {
            var listCount = _stackList.Count;
            for (var i = 0; i < listCount; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject(PoolType.Collectable);
                obj.SetActive(true);
                _manager.AddInStack(obj);
            }
        }
    }
}