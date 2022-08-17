using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Commands.ColorCheckArea
{
    public class OutLineChangeCommand 
    {
     
        public void Execute(List<GameObject> _stack,float endValue)
        {
            for (var i = 0; i < _stack.Count; i++)
            {
                var materialColor = _stack[i].GetComponentInChildren<SkinnedMeshRenderer>().material;
                materialColor.DOFloat(endValue, "_OutlineSize", 1f);
            }
        }
    }
}