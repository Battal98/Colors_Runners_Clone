using Data.UnityObject;
using UnityEngine;

namespace Commands.Pool
{
    public class RestartPoolCommand
    {
        #region Self Variables

        #region Private Variables

        private readonly CD_PoolGenerator _cdPoolGenerator;
        private readonly Transform _managerTranform;
        private readonly GameObject _levelHolder;

        #endregion

        #endregion

        public RestartPoolCommand(ref CD_PoolGenerator cdPoolGenerator, ref Transform managertransform,
            ref GameObject levelHolder)
        {
            _cdPoolGenerator = cdPoolGenerator;
            _managerTranform = managertransform;
            _levelHolder = levelHolder;
        }

        public void Execute()
        {
            var pooldata = _cdPoolGenerator.PoolObjectList;
            for (var i = 0; i < pooldata.Count; i++)
            {
                var _child = _managerTranform.GetChild(i);
                if (_child.transform.childCount > pooldata[i].ObjectCount)
                {
                    var count = _child.transform.childCount;
                    for (var j = pooldata[i].ObjectCount; j < count; j++)
                        _child.GetChild(pooldata[i].ObjectCount).SetParent(_levelHolder.transform.GetChild(0));
                }
            }
        }
    }
}