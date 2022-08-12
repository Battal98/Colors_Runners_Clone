using System.Diagnostics;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI levelText;

        #endregion

        #region Private Variables
        
        #endregion
        
        #endregion

        public void SetLevelText()
        {
            levelText.text = "Level " + (LevelSignals.Instance.onGetLevel() + 1);
        }
    }
}