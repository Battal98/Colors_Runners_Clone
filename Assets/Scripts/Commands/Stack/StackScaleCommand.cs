using System.Collections;
using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;
using DG.Tweening;


namespace Commands
{
    public class StackScaleCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stacklist;
        private StackData _stackData;

        #endregion

        #endregion

        public StackScaleCommand(ref List<GameObject> stackList, ref StackData stackData)
        {
            _stacklist = stackList;
            _stackData = stackData;
        }

        public IEnumerator Execute()
        {
            for (int i = 0; i <= _stacklist.Count - 1; i++)
            {
                int index = i;
                _stacklist[index].transform
                    .DOScale(new Vector3(1, _stackData.StackMaxScaleValue, 1), _stackData.StackScaleDelay)
                    .SetEase(Ease.Flash);
                _stacklist[index].transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), _stackData.StackScaleDelay)
                    .SetDelay(_stackData.StackTaskDelay).SetEase(Ease.Flash);
                yield return new WaitForSeconds(_stackData.StackTaskDelay / 2);
            }
        }
    }
}