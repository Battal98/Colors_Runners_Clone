using System.Collections.Generic;
using Managers;
using UnityEngine;
using Enums;

namespace Commands
{
    public class ChangeCollectableColorCommand
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private List<GameObject> _stackList = new List<GameObject>();

        #endregion

        #endregion

        public ChangeCollectableColorCommand(ref List<GameObject> stackList)
        {
            _stackList = stackList;
        }

        public void Execute(ColorType colorType)
        {
            for (int i = 0; i < _stackList.Count; i++)
            {
                _stackList[i].GetComponent<CollectableManager>().CollectableColorChange(colorType); //command olustur
            }
        }
    }
}