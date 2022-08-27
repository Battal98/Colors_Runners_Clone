using System;
using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;
using Keys;
using Signals;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Event Subscription

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            SaveSignals.Instance.onSaveData += OnSaveData;
        }

        private void Unsubscribe()
        {
            SaveSignals.Instance.onSaveData -= OnSaveData;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        #endregion

        private void OnSaveData()
        {
            SaveGame(
               
                     SaveSignals.Instance.onGetRunnerDatas(),
                
                SaveSignals.Instance.onGetIdleDatas()
            );
        }

        private void Awake()
        {
            SetReferences();
        }

        private void SetReferences()
        {
            LoadGame();
        }

        private void SaveGame(RunnerDataParams dataParams, IdleDataParams idleDataParams)
        {
            if (dataParams.Level != null) ES3.Save("Level", dataParams.Level);
            if (idleDataParams.CityLevel != null) ES3.Save("CityLevel", idleDataParams.CityLevel);
            if (idleDataParams.AreaDatas != null) ES3.Save("AreaDatas", idleDataParams.AreaDatas);
            if (idleDataParams.Score != null) ES3.Save("Score", idleDataParams.Score);
            if (idleDataParams.Score != null) ES3.Save("CompletedArea", idleDataParams.CompletedArea);
        }

        private void LoadGame()
        {
            SaveSignals.Instance.onLoadIdleData?.Invoke(
                new IdleDataParams()
                {
                    AreaDatas = ES3.KeyExists("AreaDatas")
                        ? ES3.Load<Dictionary<int, AreaData>>("AreaDatas")
                        : new Dictionary<int, AreaData>(),
                    CityLevel = ES3.KeyExists("CityLevel") ? ES3.Load<int>("CityLevel") : 0,
                    Score = ES3.KeyExists("Score") ? ES3.Load<int>("Score") : 0,
                    CompletedArea = ES3.KeyExists("CompletedArea") ? ES3.Load<int>("CompletedArea") : 0,
                }
            );
            SaveSignals.Instance.onLoadRunnerData?.Invoke(
                new RunnerDataParams()
                {
                    Level = ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0,
                }
            );
        }
    }
}