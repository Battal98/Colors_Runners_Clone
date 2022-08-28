using System.Collections.Generic;
using Datas.ValueObject;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class RandomKillInStackCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stackList;
        private StackData _stackData;
        private StackManager _manager;

        #endregion

        #endregion


        public RandomKillInStackCommand(ref StackManager stackManager, ref List<GameObject> stackList,
            ref StackData stackData)
        {
            _stackList = stackList;
            _manager = stackManager;
            _stackData = stackData;
        }

        public void Execute()
        {
            if (_stackList.Count == 0)
            {
                LevelSignals.Instance.onLevelFailed?.Invoke();
                return;
            }

            if (_stackList.Count >= _stackData.StackLimit)
            {
                _stackList[_stackData.StackLimit - 1].SetActive(true);
                _manager.CollectableAnimSet(_stackList[_stackData.StackLimit - 1], CollectableAnimationStates.Run);
            }

            if (_stackList.Count > 4)
            {
                StackSignals.Instance.onRemoveInStack?.Invoke(_stackList[Random.Range(0, 5)]);
            }
            else
            {
                StackSignals.Instance.onRemoveInStack?.Invoke(_stackList[0]);
            }
        }
    }
}