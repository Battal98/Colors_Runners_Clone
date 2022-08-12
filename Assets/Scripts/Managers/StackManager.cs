using UnityEngine;
using Controllers;
using Signals;
using Data.UnityObject;
using Commands;
using Datas.ValueObject;
using System.Collections.Generic;
using System.Collections;
using Enums;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Veriables

        #region Public Veriables

        [Header("Data")] public StackData StackData;

        #endregion

        #region Serilazible Veriables

        [SerializeField] private List<GameObject> stackList = new List<GameObject>();
        [SerializeField] private GameObject levelHolder;
        [SerializeField] private StackManager stackManager;

        #endregion

        #region Private Veriables

        private CollectableAddOnStackCommand _collectableAddOnStackCommand;
        private StackLerpMovementCommand _stackLerpMovementCommand;
        private StackScaleCommand _stackScaleCommand;
        private CollectableRemoveOnStackCommand _collectableRemoveOnStackCommand;
        private TransportInStack _transportInStack;
        private CollectableAnimSetCommand _collectableAnimSetCommand;
        private Transform _playerManager;
        private float _stackScore;

        #endregion

        #endregion

     
      

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onAddInStack += OnAddInStack;
            StackSignals.Instance.onRemoveInStack += _collectableRemoveOnStackCommand.Execute;
            StackSignals.Instance.onTransportInStack += OnTransportInStack;
            
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }


        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddInStack -= OnAddInStack;
            StackSignals.Instance.onRemoveInStack -= _collectableRemoveOnStackCommand.Execute;
            StackSignals.Instance.onTransportInStack += OnTransportInStack;

            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private StackData GetStackData()
        {
            return Resources.Load<CD_Stack>("Data/CD_Stack").Data;
        }
        private void Awake()
        {
            GetReferences();
        }
        
        private void Update()
        {
            if (!_playerManager)
                return;
            transform.position = new Vector3(0, 0, _playerManager.position.z - StackData.StackOffset);
            _stackLerpMovementCommand.Execute(ref _playerManager);
        }

        private void GetReferences()
        {
            StackData = GetStackData();
            _collectableAddOnStackCommand = new CollectableAddOnStackCommand(ref stackManager, ref stackList);
            _stackLerpMovementCommand = new StackLerpMovementCommand(ref stackList, ref StackData);
            _stackScaleCommand = new StackScaleCommand(ref stackList, ref StackData);
            _collectableRemoveOnStackCommand = new CollectableRemoveOnStackCommand(ref stackList, ref stackManager, ref levelHolder);
            _transportInStack = new TransportInStack(ref stackList,ref stackManager,ref levelHolder);
            _collectableAnimSetCommand = new CollectableAnimSetCommand();
        }
        private void FindPlayer()
        {
            if (!_playerManager)
            {
                _playerManager = FindObjectOfType<PlayerManager>().transform;
            }
        }
        private void OnAddInStack(GameObject obj)
        {
            StartCoroutine(_stackScaleCommand.Execute());
            _collectableAddOnStackCommand.Execute(obj);
        }
        private void OnTransportInStack(GameObject obj)
        {
            StartCoroutine(_transportInStack.Execute(obj));
        }
        private void Initialized()
        {
            for (int i = 0; i < stackList.Count; i++)
            {
                _collectableAnimSetCommand.Execute(stackList[i],CollectableAnimationStates.Run);
            }
            
        }

        private void OnPlay()
        {
            FindPlayer();
            Initialized();
        }
        private void OnReset()
        {
            stackList.Clear();
            stackList.TrimExcess();
        }
    }
}