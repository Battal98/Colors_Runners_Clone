using System;
using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private UIPanelController uiPanelController;
        [SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private IdlePanelController idlePanelController;
        

        #endregion

        #region Private Variables

        private int _levelCount;

        #endregion

        #endregion


        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;

            CoreGameSignals.Instance.onPlay += OnPlay;

            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;

            CoreGameSignals.Instance.onPlay -= OnPlay;

            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void Awake()
        {
            InitPanels();
        }


        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenPanel(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.ClosePanel(panelParam);
        }

        private void InitPanels()
        {
            uiPanelController.ClosePanel(UIPanels.FailPanel);
            uiPanelController.ClosePanel(UIPanels.LevelPanel);
            uiPanelController.ClosePanel(UIPanels.IdlePanel);
        }

        #region Set Text Jobs

        private void OnSetLevelText()
        {
            levelPanelController.SetLevelText();
        }

        private void SetStackText(int value)
        {
            idlePanelController.SetScoreText(value);
        }

        #endregion

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
            
        }

        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
            LevelSignals.Instance.onLevelFailed?.Invoke();
        }

        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            LevelSignals.Instance.onLevelSuccessful?.Invoke(); // Trigger in Final 
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
            OnSetLevelText();
        }

        public void NextLevel()
        {
            LevelSignals.Instance.onNextLevel?.Invoke();

            UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void RestartLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);

            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void RetryButton()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);

            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void OnClickCloseButton(GameObject _closeButtonObj)
        {
            UISignals.Instance.onOpenPanel.Invoke(UIPanels.StartPanel);
            _closeButtonObj.transform.parent.gameObject.SetActive(false);
        }
    }
}