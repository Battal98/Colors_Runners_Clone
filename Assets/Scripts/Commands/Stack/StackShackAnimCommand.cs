using System.Collections;
using System.Collections.Generic;
using Datas.ValueObject;
using DG.Tweening;
using UnityEngine;

namespace Commands
{
    public class StackShackAnimCommand
    {
        #region Self Variables

        #region Private Variables

        private readonly List<GameObject> _stacklist;
        private readonly StackData _stackData;

        #endregion

        #endregion

        public StackShackAnimCommand(ref List<GameObject> stackList, ref StackData stackData)
        {
            _stacklist = stackList;
            _stackData = stackData;
        }

        public IEnumerator Execute()
        {
            for (var i = 0; i <= _stacklist.Count - 1; i++)
            {
                var index = i;
                _stacklist[index].transform
                    .DOScale(new Vector3(1, _stackData.StackMaxScaleValue, 1), _stackData.StackScaleDelay)
                    .SetEase(Ease.Flash);
                _stacklist[index].transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), _stackData.StackScaleDelay)
                    .SetDelay(_stackData.StackShackAnimDuration).SetEase(Ease.Flash);
                yield return new WaitForSeconds(_stackData.StackShackAnimDuration / 3);
            }
        }
    }
}