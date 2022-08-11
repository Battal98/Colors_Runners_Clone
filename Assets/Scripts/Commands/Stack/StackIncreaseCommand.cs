using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class StackIncreaseCommand
    {
        public void IncreaseFunc(GameObject _obj ,GameObject _targetObj, Vector3 _pos, List<GameObject> _stackList)
        {
            _obj.transform.parent = _targetObj.transform;
            _obj.transform.localPosition = _pos;
            _stackList.Add(_obj);
        }
    }
}