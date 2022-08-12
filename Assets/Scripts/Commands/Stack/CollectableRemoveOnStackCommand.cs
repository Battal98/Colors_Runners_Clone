using System.Collections.Generic;
using Datas.ValueObject;
using Managers;
using UnityEngine;

namespace Commands
{
    public class CollectableRemoveOnStackCommand 
    {
        
        #region Self Variables

        #region Private Variables
        private List<GameObject> _stackList;
        private GameObject _levelHolder;
        private StackManager _manager;
        private StackData _stackData;

        #endregion

        #endregion
        
        public CollectableRemoveOnStackCommand(ref List<GameObject> stackList,ref StackManager manager,ref GameObject levelHolder,ref StackData stackData)
        {
            _stackList = stackList;
            _manager = manager;
            _levelHolder = levelHolder;
            _stackData = stackData;
        }
        public void Execute(GameObject collectableGameObject)
        {
            int index = _stackList.IndexOf(collectableGameObject);
            collectableGameObject.transform.SetParent(_levelHolder.transform);
            collectableGameObject.SetActive(false);
            _stackList.RemoveAt(index);
            _stackList.TrimExcess();
            if (_stackList.Count>_stackData.StackLimit)
            {
                _stackList[_stackData.StackLimit-1].SetActive(true);
                _manager.CollectableAnimSet(_stackList[_stackData.StackLimit-1]);
            }
            // _manager.StackValueUpdateCommand.StackValuesUpdate();
        }
    }
}