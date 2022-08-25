using System.Collections.Generic;
using Signals;
using UnityEngine;

namespace Commands
{
    public class CollectablesThrowCommand 
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        private List<GameObject> _stacklist;
        
        #endregion

        #endregion
        
        public CollectablesThrowCommand(ref List<GameObject> stackList)
        {

            _stacklist = stackList;


        }

        public void Execute()
        {
            if (_stacklist.Count>0)
            {
                _stacklist[0].SetActive(true);
                _stacklist.RemoveAt(0);
                _stacklist.TrimExcess();
            }
            ScoreSignals.Instance.onGetScore?.Invoke(_stacklist.Count);
        }
    }
}