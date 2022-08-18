using System.Collections;
using System.Collections.Generic;
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
                _stacklist[i].transform.DOLocalJump(
                    new Vector3(
                        _stacklist[i].transform.localPosition.x,
                        _stacklist[i].transform.localRotation.y,
                        _stacklist[i].transform.localPosition.z),
                    distance,
                    1, duration
                ).SetAutoKill();

                yield return new WaitForSeconds(duration / 3);
            }
        }
    }
}