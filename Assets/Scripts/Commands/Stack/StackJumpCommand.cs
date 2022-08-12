using System.Collections;
using System.Collections.Generic;
using Data.ValueObject;
using Datas.ValueObject;
using UnityEngine;
using DG.Tweening;

namespace Commands
{
    public class StackJumpCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stacklist;
        private StackData _stackData;
        
        #endregion

        #endregion
        public StackJumpCommand(ref List<GameObject> stackList, ref StackData stackData)
        {
            _stacklist = stackList;
            _stackData = stackData;
        }
        
        public IEnumerator Execute(float distance, float duration)
        {
            for (int i = 0; i <= _stacklist.Count - 1; i++)
            {
                int index = i;
                _stacklist[index].transform.DOMoveY(distance, duration);
                //_stacklist[index].transform.DOMoveY(new Vector3(0,_stackData.StackJumpDistance,1), _stackData.StackJumpDuration);
                //_stacklist[index].transform.DOScale(new Vector3(0.8f,0.8f,0.8f), _stackData.StackScaleDelay).SetDelay(_stackData.StackTaskDelay).SetEase(Ease.Flash);
                _stacklist[index].transform.DOMoveY(0, 0.10f);
                yield return new WaitForSeconds(_stackData.StackJumpDuration);
            }
        }
    }
}