using Data.UnityObject;
using Datas.ValueObject;
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

        #endregion

        #region Private Variables

        private BuildData _buildData;
        private AreaStageType _areaStageType;

        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _buildData = GetData();
            buildCost.text = _buildData.BuildCost.ToString();
            gardenCost.text = _buildData.GardenCost.ToString();
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

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
           
        }

        private void UnSubscribeEvents()
        {
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
         
        }

        #endregion

        private void OnLevelSuccessful()
        {
        }
        
        private void GetStageCost()
        {
        }

        private void SetStageCost()
        {
        }

        private void AreaBuildController()
        {
        }

        private void GetBuildData()
        {
        }
    }
}