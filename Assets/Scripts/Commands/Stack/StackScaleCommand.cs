using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace Commands
{
    public class StackScaleCommand
    {
        Tween tween;

        public IEnumerator ScaleSizeUpAndDown(List<GameObject> _stackList, float _maxScaleValueData,float _scaleDelay,float _stackDelay)
        {
            for (int i = 0; i <= _stackList.Count - 1; i++)
            {
                int index = i;
                _stackList[index].transform.DOScale(new Vector3(1,_maxScaleValueData,1), _scaleDelay).SetEase(Ease.Flash);
                _stackList[index].transform.DOScale(new Vector3(0.8f,0.8f,0.8f), _scaleDelay).SetDelay(_stackDelay).SetEase(Ease.Flash);
                yield return new WaitForSeconds(_stackDelay/2);
            }
        }
    }
}