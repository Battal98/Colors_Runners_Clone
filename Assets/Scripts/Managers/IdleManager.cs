using System;
using System.Collections.Generic;
using Data.UnityObject;
using Datas.ValueObject;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class IdleManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject cityHolder;

        #endregion

        #region Private Variables

        [ShowInInspector] private Dictionary<AreaData, int> _areaDictionary = new Dictionary<AreaData, int>(7);
        private int _cityLevel;
        private CD_IdleData _cdIdleData;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _cdIdleData = GetIdleData();
            //_areaDictionary = LoadAreaData();
        }

        private CD_IdleData GetIdleData() => Resources.Load<CD_IdleData>("Data/CD_IdleData");

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            IdleGameSignals.Instance.onAreaComplete += OnAreaComplete;
        }

        private void UnSubscribeEvent()
        {
            IdleGameSignals.Instance.onAreaComplete += OnAreaComplete;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private void Start()
        {
            OnInitializeLevel();
        }

        private void OnAreaComplete()
        {
        }

        private void OnInitializeLevel()
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/CityPrefabs/City {_cityLevel}"),
                cityHolder.transform);
        }
    }
}