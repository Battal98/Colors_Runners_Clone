using System;
using Controllers;
using Enums;
using Signals;
using UnityEngine;
using Data.ValueObject;
using Data.UnityObject;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField]
        private UIPanelController uiPanelController;

        [SerializeField]
        private LevelPanelController levelPanelController;

        [SerializeField]
        private IdlePanelController idlePanelController;

        #endregion

        #region Private Variables

        private int _levelCount;

        #endregion

        #endregion

        private void Awake()
        {
            InitPanels();
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;

            #region CoreGameSignals Subscription

            CoreGameSignals.Instance.onPlay += OnPlay;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onGetLevelID += OnSetLevelText;
           
            #endregion

            #region SaveLoadSignals Subscription

            #endregion


        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;

            #region CoreGameSignals Unsubscription

            // CoreGameSignals Came here with onplay, onlevelfailed, onlevelSuccess
            
            CoreGameSignals.Instance.onPlay -= OnPlay;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onGetLevelID -= OnSetLevelText;
            

            #endregion

            #region SaveLoadSignals Unsubscription


            #endregion

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


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

        private void OnSetLevelText(int value)
        {
            levelPanelController.SetLevelText(value);
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
            LevelSignals.Instance.onLevelSuccessful?.Invoke();// Trigger in Final 
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