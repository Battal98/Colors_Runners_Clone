using System.Collections.Generic;
using Data.UnityObject;
using Datas.ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class IdleManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject cityHolder;

        #endregion

        #region Private Variables

        [ShowInInspector] private Dictionary<int, AreaData> _areaDictionary = new Dictionary<int, AreaData>(7);
        private int _cityLevel;
        private CD_IdleData _cdIdleData;
        private int _completedArea;
        private bool _levelIsPlayable;
        private int _score;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _cdIdleData = GetIdleData();
        }

        private CD_IdleData GetIdleData()
        {
            return Resources.Load<CD_IdleData>("Data/CD_IdleData");
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            IdleGameSignals.Instance.onAreaComplete += OnAreaComplete;
            IdleGameSignals.Instance.onSetAreaData += OnSetAreaData;
            IdleGameSignals.Instance.onGetAreaData += OnGetAreaData;

            LevelSignals.Instance.onNextLevel += OnNextLevel;

            SaveSignals.Instance.onGetIdleDatas += OnGetIdleDatas;
            SaveSignals.Instance.onLoadIdleData += OnLoadIdleData;

            CoreGameSignals.Instance.onPlay += OnPlay;

            ScoreSignals.Instance.onGetIdleScore += OnGetIdleScore;
            ScoreSignals.Instance.onSetIdleScore += OnSetIdleScore;

        }

        private void UnSubscribeEvent()
        {
            IdleGameSignals.Instance.onAreaComplete -= OnAreaComplete;
            IdleGameSignals.Instance.onSetAreaData -= OnSetAreaData;
            IdleGameSignals.Instance.onGetAreaData -= OnGetAreaData;

            LevelSignals.Instance.onNextLevel -= OnNextLevel;

            SaveSignals.Instance.onGetIdleDatas -= OnGetIdleDatas;
            SaveSignals.Instance.onLoadIdleData -= OnLoadIdleData;

            CoreGameSignals.Instance.onPlay -= OnPlay;
            
            ScoreSignals.Instance.onGetIdleScore -= OnGetIdleScore;
            ScoreSignals.Instance.onSetIdleScore -= OnSetIdleScore;

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

        private IdleDataParams OnGetIdleDatas()=> new IdleDataParams
        {
            AreaDatas = _areaDictionary,
            CityLevel = _cityLevel,
            Score = _score,
            CompletedArea = _completedArea
        };

        private int OnGetIdleScore() => _score;

        private void OnSetIdleScore(int score)
        {
            _score = score;
            UISignals.Instance.onSetScoreText?.Invoke(_score);

        }
      

        
        
        private void OnAreaComplete()
        {
            _completedArea++;
            CityCompleteCheck();
        }

        private void OnInitializeLevel()
        {
            Instantiate(
                Resources.Load<GameObject>($"Prefabs/CityPrefabs/City {_cityLevel % _cdIdleData.DataList.Count}"),
                cityHolder.transform);
        }
        private AreaData OnGetAreaData(int id)=>_areaDictionary.ContainsKey(id) ? _areaDictionary[id] : new AreaData();

        private void OnSetAreaData(int id, AreaData AraeData)
        {
            if (_areaDictionary.ContainsKey(id))
                _areaDictionary[id] = AraeData;
            else
                _areaDictionary.Add(id, AraeData);

            SaveSignals.Instance.onSaveData?.Invoke();
        }

        

        private void CityCompleteCheck()
        {
            if (_completedArea == _cdIdleData.DataList[_cityLevel].BuildCount)
            {
                _cityLevel++;
                _levelIsPlayable = true;
                _completedArea = 0;
            }
        }

        private void OnNextLevel()
        {
            if (_levelIsPlayable)
            {
                _areaDictionary.Clear();
                Destroy(cityHolder.transform.GetChild(0).gameObject);
                OnInitializeLevel();
                _levelIsPlayable = false;
            }
            else
            {
                IdleGameSignals.Instance.onPrepareAreaWithSave?.Invoke();
            }
            
        }

        private void OnLoadIdleData(IdleDataParams idleDataParams)
        {
            _areaDictionary = idleDataParams.AreaDatas;
            _cityLevel = idleDataParams.CityLevel;
            _score = idleDataParams.Score;
            _completedArea = idleDataParams.CompletedArea;
        }
    
        private void OnPlay()
        {
            IdleGameSignals.Instance.onRefresthAreaData?.Invoke();
        }
    }
}