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

        #region Serialized Variables

        [SerializeField] private UIPanelController uiPanelController;
        [SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private IdlePanelController idlePanelController;

        #endregion

        #region Private Variables

  

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
            UISignals.Instance.onSetScoreText += onSetScoreText;
            CoreGameSignals.Instance.onGetGameState += OnGetGameState;
            CoreGameSignals.Instance.onPlay += OnPlay;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetScoreText -= onSetScoreText;
            CoreGameSignals.Instance.onGetGameState -= OnGetGameState;
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

        private void Start()
        {
            OnSetLevelText();
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

        private void OnSetLevelText()
        {
            levelPanelController.SetLevelText();
        }

        private void onSetScoreText(int value)
        {
            idlePanelController.SetScoreText(value);
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        }

        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            LevelSignals.Instance.onLevelSuccessful?.Invoke(); // Trigger in Final 
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void NextLevel()
        {
            LevelSignals.Instance.onNextLevel?.Invoke();
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            OnSetLevelText();
        }

        public void Restart()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            LevelSignals.Instance.onRestartLevel?.Invoke();
        }

        public void WinPanelClose()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.IdlePanel);
        }

        private void OnGetGameState(GameStates states)
        {
            switch (states)
            {
                case GameStates.Idle:
                    UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
                    UISignals.Instance.onOpenPanel?.Invoke(UIPanels.WinPanel);
                    break;
                case GameStates.Runner:
                    UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
                    UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
                    UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
                    break;
            }
        }
    }
}