using System;
using Commands.Level;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
        
        [Header("Data")] public int Data;
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] private GameObject levelHolder;
       
        #endregion

        #region Private Variables

        private ClearActiveLevelCommand _clearActiveLevel;
        private LevelLoaderCommand _levelLoader;
        [ShowInInspector] private int _levelID;

        #endregion

        #endregion

        private void Awake()
        {
            _levelID = GetActiveLevel();
            _clearActiveLevel = new ClearActiveLevelCommand();
            _levelLoader = new LevelLoaderCommand();
            Data = GetLevelCount();
        }

        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }
        private int GetLevelCount()
        {
            return _levelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize += OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
            LevelSignals.Instance.onRestartLevel += OnRestartLevel;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
            LevelSignals.Instance.onRestartLevel -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Start()
        {
            OnInitializeLevel();
        }
        private void OnInitializeLevel()
        {
            int newLevelData = GetLevelCount();
            _levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
        }
        private void OnClearActiveLevel()
        {
            _clearActiveLevel.ClearActiveLevel(levelHolder.transform);
        }

        private void OnLevelFailed()
        {
            //öldüğünde ui fln cnm
        }

        private void OnLevelSuccessful()
        {
            //böyle bir şey yok oyunda????
        }
        private void OnNextLevel()
        {
            _levelID++;
            LevelSignals.Instance.onClearActiveLevel?.Invoke();
            //CoreGameSignals.Instance.onReset?.Invoke();
            //SaveSignals.Instance.onSaveGameData?.Invoke();
            LevelSignals.Instance.onLevelInitialize?.Invoke();
            //SetLevelText();
        }
        private void OnRestartLevel()
        {
            LevelSignals.Instance.onClearActiveLevel?.Invoke();
            //CoreGameSignals.Instance.onReset?.Invoke();
            //SaveSignals.Instance.onSaveGameData?.Invoke();
            LevelSignals.Instance.onLevelInitialize?.Invoke();
        }
        
    }
}
