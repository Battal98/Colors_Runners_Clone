using Data.UnityObject;
using UnityEngine;

namespace Commands.Pool
{
    public class PoolGenerateCommand
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        private CD_PoolGenerator _cdPoolGenerator;
        private GameObject _emptyGameObject;
        private Transform _managerTranform;

        #endregion

        #endregion

        public PoolGenerateCommand(ref CD_PoolGenerator cdPoolGenerator, ref Transform managertransform,
            ref GameObject emptyGameObject)
        {
            _cdPoolGenerator = cdPoolGenerator;
            _emptyGameObject = emptyGameObject;
            _managerTranform = managertransform;
        }

        public void Execute()
        {
            var pooldata = _cdPoolGenerator.PoolObjectList;
            for (int i = 0; i < pooldata.Count; i++)
            {
                _emptyGameObject = new GameObject();
                _emptyGameObject.transform.parent = _managerTranform;
                _emptyGameObject.name = pooldata[i].ObjName;

                for (int j = 0; j < pooldata[i].ObjectCount; j++)
                {
                    var obj = Object.Instantiate(pooldata[i].Pref, _managerTranform.GetChild(i));
                    obj.SetActive(false);
                }
            }
        }
    }
}