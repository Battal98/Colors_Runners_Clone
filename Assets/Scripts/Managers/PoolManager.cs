using System;
using System.ComponentModel;
using Data.UnityObject;
using Signals;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables
        [SerializeField]private GameObject levelHolder;

        #endregion

        #region Private Variables

        private CD_PoolGenerator _poolGenerator;
        private GameObject _emptyGameObject;

        #endregion

        #endregion


        private void Awake()
        {
            
            GetReferences();
            Init();
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            LevelSignals.Instance.onRestartLevel += OnRestartLevel;
        }

        private void UnSubscribeEvent()
        {
            LevelSignals.Instance.onRestartLevel -= OnRestartLevel;
        }
        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private void GetReferences()
        {
            _emptyGameObject = new GameObject();
            _poolGenerator = GetPoolData();
        }

        private void Init()
        {
            var pooldata = _poolGenerator.PoolObjectList;
            for (int i = 0; i < pooldata.Count; i++)
            {
                var obj=Instantiate(_emptyGameObject,transform);
                obj.name = pooldata[i].ObjName;
                for (int j = 0; j < pooldata[i].ObjectCount; j++)
                {
                   obj=  Instantiate(pooldata[i].Pref,transform.GetChild(i));
                  obj.SetActive(false);
                }
            }
        }

        private CD_PoolGenerator GetPoolData() => Resources.Load<CD_PoolGenerator>("Data/CD_PoolGenerator");

        private void RestartPool()
        {
            var pooldata = _poolGenerator.PoolObjectList;
            for (int i = 0; i < pooldata.Count; i++)
            {
                var _child = transform.GetChild(i);
                if (_child.transform.childCount>pooldata[i].ObjectCount)
                {
                    int count = _child.transform.childCount;
                    for (int j = pooldata[i].ObjectCount; j < count; j++)
                    {
                        _child.GetChild(pooldata[i].ObjectCount).SetParent(levelHolder.transform);
                    }
                }
            }
        }
        private void OnRestartLevel()
        {
            RestartPool();
        }

      
    }
}