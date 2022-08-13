using Datas.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class AreaManager : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
        
        [Header("Data")] public BuildData BuildData;
        
        #endregion
    
        #region Serialized Variables

        #endregion
    
        #region Private Variables

        private BuildType _buildType;
        private AreaStageType _areaStageType;
        
        #endregion
    
        #endregion
        
        
        
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
            IdleGameSignals.Instance.onAreaComplete += OnAreaComplete;

        }
        private void UnSubscribeEvents()
        {
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            IdleGameSignals.Instance.onAreaComplete -= OnAreaComplete;
        }
        
        
        
        #endregion

        private void OnLevelSuccessful()
        {
            
        }   
        private void OnAreaComplete(int i)
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