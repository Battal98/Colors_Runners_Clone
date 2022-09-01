using System;
using DG.Tweening;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI multiplierText;
        

        #endregion

        #endregion

      

        public void SetLevelText()
        {
            levelText.text = "Level " + (LevelSignals.Instance.onGetLevel() + 1);
        }

        public void SetMultipler()
        {       
            multiplierText.alpha=1;
            multiplierText.DOFade(0f,2f).SetEase(Ease.InQuad);
        }
    }
}