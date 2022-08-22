using Data.UnityObject;
using UnityEngine;

namespace Commands.Pool
{
    public class RestartPoolCommand
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private CD_PoolGenerator _cdPoolGenerator;
    
        private Transform _managerTranform;
        private GameObject _levelHolder;

        #endregion

        #endregion

        public RestartPoolCommand(ref CD_PoolGenerator cdPoolGenerator, ref Transform managertransform,
            ref GameObject levelHolder)
        {
            _cdPoolGenerator=cdPoolGenerator;
           
            _managerTranform=managertransform;
            _levelHolder=levelHolder;
        }

        public void Execute()
        {
            var pooldata = _cdPoolGenerator.PoolObjectList;
            for (int i = 0; i < pooldata.Count; i++)
            {
                var _child = _managerTranform.GetChild(i);
                if (_child.transform.childCount > pooldata[i].ObjectCount)
                {
                    int count = _child.transform.childCount;
                    for (int j = pooldata[i].ObjectCount; j < count; j++)
                    {
                        _child.GetChild(pooldata[i].ObjectCount).SetParent(_levelHolder.transform.GetChild(0));
                    }
                }
            }
        }
    }
}