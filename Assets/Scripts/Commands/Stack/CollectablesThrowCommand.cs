using System.Collections.Generic;
using Data.ValueObject;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Commands
{
    public class CollectablesThrowCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _temlList;

        #endregion

        #endregion

        public CollectablesThrowCommand(ref List<GameObject> tempList)
        {
            _temlList = tempList;
        }

        public void Execute(Transform PlayerTranform)
        {
            if (_temlList.Count > 0)
            {
                _temlList[0].SetActive(true);
                _temlList[0].transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0);
                _temlList[0].transform.position = PlayerTranform.position;
                _temlList[0].transform
                    .DOJump(
                        new Vector3(PlayerTranform.position.x + Random.Range(-1f, 1f), PlayerTranform.position.y,
                            PlayerTranform.transform.position.z + 2f), 2, 1, 1.5f);
                _temlList.RemoveAt(0);
                _temlList.TrimExcess();
            }

            ScoreSignals.Instance.onGetScore?.Invoke(_temlList.Count);
        }
    }
}