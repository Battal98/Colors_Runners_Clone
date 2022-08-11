using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Commands
{
    public class TransportInStack 
    {
        #region Self Variables
        #region Private Variables

        private StackManager _stackManager;
        private List<GameObject> _stacklist;
        private GameObject _levelHolder;

        #endregion
        #endregion
        
        public TransportInStack(ref List<GameObject> stacklist,ref StackManager stackManager,ref GameObject levelholder)
        {
            _stacklist = stacklist;
            _stackManager = stackManager;
            _levelHolder = levelholder;
        }
        
      
        public IEnumerator Execute(GameObject _obj)
        {
            yield return new WaitForSeconds(0.15f);
            _obj.transform.parent = _levelHolder.transform;
            _stacklist.Remove(_obj);
            _stacklist.TrimExcess();
        }
    }
}