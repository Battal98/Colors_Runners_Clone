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
        private GameObject _levelHolder;
        private StackData _stackData;
        
    #endregion
        #endregion
        
        public TransportInStack(ref List<GameObject> stackList,ref StackManager manager,ref GameObject levelholder,ref StackData stackdata)
        {
            _stackList = stackList;
            _manager = manager;
            _levelHolder = levelholder;
            _stackData = stackdata;
        }
        
      
        public IEnumerator Execute(GameObject _obj)
        { 
            _obj.transform.parent = _levelHolder.transform;
            yield return new WaitForSeconds(0.15f);
            _stackList.Remove(_obj);
            _stackList.TrimExcess();
            if (_stackList.Count>=_stackData.StackLimit)
            {
                _stackList[_stackData.StackLimit-1].SetActive(true);
                _manager.CollectableAnimSet(_stackList[_stackData.StackLimit-1]);
            }
        }
    }
}