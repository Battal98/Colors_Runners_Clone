using Commands;
using Data.UnityObject;
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

        [Header("Data")] public int LevelData;

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
            GetReferences();
            Init();
        }

        private void GetReferences()
        {
            LevelData = GetLevelCount();
        }

        private void Init()
        {
            _clearActiveLevel = new ClearActiveLevelCommand(ref levelHolder);
            _levelLoader = new LevelLoaderCommand(ref levelHolder);
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
            LevelSignals.Instance.onNextLevel += OnNextLevel;
            LevelSignals.Instance.onRestartLevel += OnRestartLevel;
            LevelSignals.Instance.onGetLevel += OnGetLevel;
            SaveSignals.Instance.onGetRunnerDatas += OnGetRunnerData;
            SaveSignals.Instance.onLoadRunnerData += OnLoadRunnerData;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
            LevelSignals.Instance.onRestartLevel -= OnRestartLevel;
            LevelSignals.Instance.onGetLevel -= OnGetLevel;
            SaveSignals.Instance.onGetRunnerDatas -= OnGetRunnerData;
            SaveSignals.Instance.onLoadRunnerData -= OnLoadRunnerData;
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

        private int OnGetLevel()
        {
            return _levelID;
        }

        private RunnerDataParams OnGetRunnerData()
        {
            return new RunnerDataParams { Level = _levelID };
        }

        private void OnLoadRunnerData(RunnerDataParams runnerDataParams)
        {
            _levelID = runnerDataParams.Level;
        }

        private int GetLevelCount()
        {
            return _levelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
        }

        private void OnInitializeLevel()
        {
            var newLevelData = GetLevelCount();
            _levelLoader.Execute(newLevelData);
        }

        private void OnClearActiveLevel()
        {
            _clearActiveLevel.Execute();
        }

        private void OnNextLevel()
        {
            _levelID++;
            LevelSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            LevelSignals.Instance.onLevelInitialize?.Invoke();
            SaveSignals.Instance.onSaveData?.Invoke();
        }

        private void OnRestartLevel()
        {
            LevelSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            LevelSignals.Instance.onLevelInitialize?.Invoke();
        }
    }
}