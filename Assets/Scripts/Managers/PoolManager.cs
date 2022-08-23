using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Commands.Pool;
using Data.UnityObject;
using Enums;
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

        [SerializeField] private GameObject levelHolder;
        [SerializeField] private Transform PoolManagerG;

        #endregion

        #region Private Variables

        private CD_PoolGenerator _cdPoolGenerator;
        private GameObject _emptyGameObject;
        private PoolGenerateCommand _poolGenerateCommand;
        private RestartPoolCommand _restartPoolCommand;

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
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
            PoolSignals.Instance.onSendPool += OnSendPool;
        }

        private void UnSubscribeEvent()
        {
            LevelSignals.Instance.onRestartLevel -= OnRestartLevel;
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
            PoolSignals.Instance.onSendPool -= OnSendPool;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private void GetReferences()
        {
            _cdPoolGenerator = GetPoolData();
        }

        private void Init()
        {
            _poolGenerateCommand =
                new PoolGenerateCommand(ref _cdPoolGenerator, ref PoolManagerG, ref _emptyGameObject);
            _restartPoolCommand = new RestartPoolCommand(ref _cdPoolGenerator, ref PoolManagerG, ref levelHolder);
        }

        private void Start()
        {
            StartPool();
        }

        private void StartPool()
        {
            _poolGenerateCommand.Execute();
        }

        private CD_PoolGenerator GetPoolData() => Resources.Load<CD_PoolGenerator>("Data/CD_PoolGenerator");

        private void RestartPool()
        {
            _restartPoolCommand.Execute();
        }

        private GameObject OnGetPoolObject(PoolType poolType)
        {
            var parent = transform.GetChild((int)poolType);
            return parent.childCount != 0
                ? parent.transform.GetChild(0).gameObject
                : Instantiate(_cdPoolGenerator.PoolObjectList[(int)poolType].Pref, parent);
        }

        private async void OnSendPool(GameObject CollectableObject, PoolType poolType)
        {
            CollectableObject.transform.parent = transform.GetChild((int)poolType);
            await Task.Delay(1500);
            CollectableObject.transform.position = Vector3.zero;
            CollectableObject.GetComponentInChildren<Collider>().enabled = true;
            CollectableObject.SetActive(false);
          
        }

        private void OnRestartLevel()
        {
            RestartPool();
        }
    }
}