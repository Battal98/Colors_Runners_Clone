using System.Collections.Generic;
using DG.Tweening;

using UnityEngine;

namespace Commands
{
    public class OutLineChangeCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stack;

        #endregion

        #endregion
        public OutLineChangeCommand(ref List<GameObject> stack)
        {
            _stack = stack;
        }
        public void Execute( float endValue)
        {
            for (var i = 0; i < _stack.Count; i++)
            {
                var materialColor = _stack[i].GetComponentInChildren<SkinnedMeshRenderer>().material;
                materialColor.DOFloat(endValue, "_OutlineSize", 1f);
            }
        }
    }
}