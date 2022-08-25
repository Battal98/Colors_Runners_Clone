using System.Collections;
using System.Collections.Generic;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Commands
{
    public class StackItemsCombineCommand
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        

        #endregion

        #region Private Variables

        private List<GameObject> _stackList;
        private List<GameObject> _tempList;
        private StackData _stackData;
        private StackManager _manager;
        
        

        #endregion

        #endregion

        public StackItemsCombineCommand(ref List<GameObject> stackList,ref StackData stackData,ref StackManager manager,ref List<GameObject> tempList)
        {

            _stackList = stackList;
            _stackData = stackData;
            _manager = manager;
            _tempList = tempList;
        }

        public IEnumerator Execute()
        {
            int count=_stackList.Count;
            for (int i = 0; i < count; i++)
            {
               
                _stackList[0].transform.DOScale(Vector3.zero, 0.10f);
                yield return new WaitForSeconds(0.15f);
                _stackList[0].SetActive(false);
                  _tempList.Add(_stackList[0]);
                _stackList.RemoveAt(0); 
                _stackList.TrimExcess(); 
               
                if (_stackList.Count >= _stackData.StackLimit)
                {
                    _stackList[_stackData.StackLimit - 1].SetActive(true);
                  
                }

            }

         
        }
    }
}