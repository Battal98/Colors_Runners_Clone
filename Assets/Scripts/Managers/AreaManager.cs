using System;
using Data.UnityObject;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class AreaManager : MonoBehaviour
    {
        #region Self Variables

        [Header("Data")]

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private BuildType buildType;

        [SerializeField] private TextMeshPro buildCost;
        [SerializeField] private TextMeshPro gardenCost;
        [SerializeField] private GameObject buildCostArea;
        [SerializeField] private GameObject gardenCostArea;
        [SerializeField] private int areaId;

        #endregion

        #region Private Variables

        private BuildData _buildData;
        private GameObject _areaCheck;
        private AreaData _areaData;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void GetReferences()
        {
            _buildData = GetData();
            buildCost.text = _buildData.BuildCost.ToString();
            gardenCost.text = _buildData.GardenCost.ToString();
        }

        private void Init()
        {
        }

        private BuildData GetData()
        {
            return Resources.Load<CD_BuildData>("Data/CD_BuildData").BuildData[(int)buildType];
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            IdleGameSignals.Instance.onCheckArea += OnCheckArea;
            IdleGameSignals.Instance.onCostDown += OnCostDown;
            IdleGameSignals.Instance.onRefreshAreaData += OnRefreshAreaData;
            IdleGameSignals.Instance.onPrepareAreaWithSave += OnPrepareAreaWithSave;
            IdleGameSignals.Instance.onCityComplete += OnCityComplete;
        }

        private void UnSubscribeEvents()
        {
            IdleGameSignals.Instance.onCheckArea -= OnCheckArea;
            IdleGameSignals.Instance.onCostDown -= OnCostDown;
            IdleGameSignals.Instance.onRefreshAreaData -= OnRefreshAreaData;
            IdleGameSignals.Instance.onPrepareAreaWithSave -= OnPrepareAreaWithSave;
            IdleGameSignals.Instance.onCityComplete -= OnCityComplete;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        private void OnRefreshAreaData()
        {
            _areaData = (AreaData)IdleGameSignals.Instance.onGetAreaData?.Invoke(areaId);
            SetMaterialColor();
            SetAreaTexts();
            CostAreaVisible();
        }

        private void OnCostDown()
        {
            if (_areaCheck != gameObject) return;
            switch (_areaData.Type)
            {
                case AreaStageType.House:
                    _areaData.BuildMaterialValue++;
                    SetAreaTexts();
                    SetMaterialColor();
                    if (_buildData.BuildCost == _areaData.BuildMaterialValue) ChangeStage();
                    break;
                case AreaStageType.Garden:
                    _areaData.GardenMaterialValue++;
                    SetAreaTexts();
                    SetMaterialColor();
                    if (_buildData.GardenCost == _areaData.GardenMaterialValue) ChangeStage();
                    break;
                case AreaStageType.Complete:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetMaterialColor()
        {
            switch (_areaData.Type)
            {
                case AreaStageType.House:
                    _buildData.BuildMaterial.DOFloat(2 / (_buildData.BuildCost / _areaData.BuildMaterialValue),
                        "_Saturation", 0.5f);
                    break;
                case AreaStageType.Garden:
                    _buildData.GardenMaterial.DOFloat(2 / (_buildData.GardenCost / _areaData.GardenMaterialValue),
                        "_Saturation", 0.5f);
                    break;
                case AreaStageType.Complete:
                    _buildData.BuildMaterial.DOFloat(2 / (_buildData.BuildCost / _areaData.BuildMaterialValue),
                        "_Saturation", 0.5f);
                    _buildData.GardenMaterial.DOFloat(2 / (_buildData.GardenCost / _areaData.GardenMaterialValue),
                        "_Saturation", 0.5f);
                    break;
            }
        }

        private void SetAreaTexts()
        {
            switch (_areaData.Type)
            {
                case AreaStageType.House:
                    buildCost.text = (_buildData.BuildCost - _areaData.BuildMaterialValue).ToString();
                    break;
                case AreaStageType.Garden:
                    gardenCost.text = (_buildData.GardenCost - _areaData.GardenMaterialValue).ToString();
                    break;
            }
        }

        private void ChangeStage()
        {
            if (_areaData.Type == AreaStageType.House)
            {
                IdleGameSignals.Instance.onStageChanged?.Invoke();
                _areaData.Type = AreaStageType.Garden;
                CostAreaVisible();
            }
            else if (_areaData.Type == AreaStageType.Garden)
            {             
                IdleGameSignals.Instance.onStageChanged?.Invoke();
                _areaData.Type = AreaStageType.Complete;
                CostAreaVisible();
                IdleGameSignals.Instance.onAreaComplete?.Invoke();
            }
            else
            {
                CostAreaVisible();
            }
        }

        private void CostAreaVisible()
        {
            switch (_areaData.Type)
            {
                case AreaStageType.House:
                    buildCostArea.SetActive(true);
                    gardenCostArea.SetActive(false);
                    break;
                case AreaStageType.Garden:
                    buildCostArea.SetActive(false);
                    gardenCostArea.SetActive(true);
                    break;
                case AreaStageType.Complete:
                    buildCostArea.SetActive(false);
                    gardenCostArea.SetActive(false);
                    break;
            }
        }

        private void OnCheckArea(GameObject Check)
        {
            _areaCheck = Check;
        }

        private void OnPrepareAreaWithSave()
        {
            IdleGameSignals.Instance.onSetAreaData?.Invoke(areaId, _areaData);
        }

        private void OnCityComplete()
        {
            _buildData.BuildMaterial.DOFloat(0, "_Saturation", 0);
            _buildData.GardenMaterial.DOFloat(0, "_Saturation", 0);
        }
    }
}