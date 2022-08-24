using System.Collections.Generic;
using Datas.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class CollectableRemoveOnStackCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stackList;
       
        private StackManager _manager;
        private StackData _stackData;

        #endregion

        #endregion

        public CollectableRemoveOnStackCommand(ref List<GameObject> stackList, ref StackManager manager,
             ref StackData stackData)
        {
            _stackList = stackList;
            _manager = manager;
            _stackData = stackData;
        }

        public void Execute(GameObject collectableGameObject)
        {
            int index = _stackList.IndexOf(collectableGameObject);
            collectableGameObject.GetComponent<CollectableManager>().SetAnim(CollectableAnimationStates.Dead);
            PoolSignals.Instance.onSendPool?.Invoke(collectableGameObject,PoolType.Collectable);
            
            _stackList.RemoveAt(index);
            _stackList.TrimExcess();
            if (_stackList.Count >= _stackData.StackLimit)
            {
                _stackList[_stackData.StackLimit - 1].SetActive(true);
                _manager.CollectableAnimSet(_stackList[_stackData.StackLimit - 1]);
            }

            if (_stackList.Count==0)
            {
                LevelSignals.Instance.onLevelFailed?.Invoke();
            }
        
        }
    }
}