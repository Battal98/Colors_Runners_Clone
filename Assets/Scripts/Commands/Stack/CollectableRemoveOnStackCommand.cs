using System.Collections.Generic;
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

        #endregion

        #endregion
        
        public CollectableRemoveOnStackCommand(ref List<GameObject> stackList,ref StackManager manager,ref GameObject levelHolder)
        {
            _stackList = stackList;
            _manager = manager;
            _levelHolder = levelHolder;
        }
        public void Execute(GameObject collectableGameObject)
        {
            int index = _stackList.IndexOf(collectableGameObject);
            collectableGameObject.transform.SetParent(_levelHolder.transform);
            collectableGameObject.SetActive(false);
            _stackList.RemoveAt(index);
            _stackList.TrimExcess();
            // _manager.StackValueUpdateCommand.StackValuesUpdate();
        }
    }
}