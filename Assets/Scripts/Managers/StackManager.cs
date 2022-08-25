using System;
using UnityEngine;
using Signals;
using Data.UnityObject;
using Commands;
using Datas.ValueObject;
using System.Collections.Generic;
using Controller;
using Enums;
using Sirenix.OdinInspector;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public StackData StackData;

        #endregion

        #region Serilazible Variables

        [SerializeField] private List<GameObject> stackList = new List<GameObject>();
        [SerializeField] private StackManager stackManager;

        #endregion

        #region Private Variables

        private CollectableAddOnStackCommand _collectableAddOnStackCommand;
        private StackLerpMovementCommand _stackLerpMovementCommand;
        private StackScaleCommand _stackScaleCommand;
        private CollectableRemoveOnStackCommand _collectableRemoveOnStackCommand;
        private TransportInStack _transportInStack;
        private CollectableAnimSetCommand _collectableAnimSetCommand;
        private ChangeCollectableColorCommand _changeCollectableColorCommand;
        private RandomKillInStackCommand _randomKillInStackCommand;
        private CollectablesThrowCommand _collectablesThrowCommand;
        private Transform _playerManager;
        [ShowInInspector] private ColorType _type;

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
            StackSignals.Instance.onRemoveInStack += OnRemoveInStack;
            StackSignals.Instance.onTransportInStack += _transportInStack.Execute;
            StackSignals.Instance.onGetStackList += OnGetStackList;
            StackSignals.Instance.onChangeCollectableColor += OnChangeCollectableColor;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
            StackSignals.Instance.onGetColorType += OnGetColorType;
            CoreGameSignals.Instance.onExitColorCheckArea += OnExitColorCheckArea;
            StackSignals.Instance.onKillRandomInStack += _randomKillInStackCommand.Execute;
            StackSignals.Instance.onCollectablesThrow += _collectablesThrowCommand.Execute;
        }


        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddInStack -= OnAddInStack;
            StackSignals.Instance.onRemoveInStack -= OnRemoveInStack;
            StackSignals.Instance.onTransportInStack -= _transportInStack.Execute;
            StackSignals.Instance.onGetStackList -= OnGetStackList;
            StackSignals.Instance.onChangeCollectableColor -= OnChangeCollectableColor;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            StackSignals.Instance.onGetColorType -= OnGetColorType;
            CoreGameSignals.Instance.onExitColorCheckArea -= OnExitColorCheckArea;
            StackSignals.Instance.onKillRandomInStack -= _randomKillInStackCommand.Execute;
            StackSignals.Instance.onCollectablesThrow -= _collectablesThrowCommand.Execute;
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
            Init();
        }

        private void GetReferences()
        {
            StackData = GetStackData();
        }

        private void Init()
        {
            _collectableAddOnStackCommand =
                new CollectableAddOnStackCommand(ref stackManager, ref stackList, ref StackData);
            _stackLerpMovementCommand = new StackLerpMovementCommand(ref stackList, ref StackData);
            _stackScaleCommand = new StackScaleCommand(ref stackList, ref StackData);
            _collectableRemoveOnStackCommand = new CollectableRemoveOnStackCommand(ref stackList, ref stackManager,
                ref StackData);
            _transportInStack = new TransportInStack(ref stackList, ref stackManager, ref StackData);
            _collectableAnimSetCommand = new CollectableAnimSetCommand();
            _changeCollectableColorCommand = new ChangeCollectableColorCommand(ref stackList);
            _randomKillInStackCommand = new RandomKillInStackCommand(ref stackManager, ref stackList,
                ref StackData);
            _collectablesThrowCommand = new CollectablesThrowCommand(ref stackList);
        }

        private void Update()
        {
            if (!_playerManager)
                return;
            _stackLerpMovementCommand.Execute(ref _playerManager);
        }

        private void FindPlayer()
        {
            if (!_playerManager)
            {
                _playerManager = FindObjectOfType<PlayerManager>().transform;
            }
        }

        private ColorType OnGetColorType() => _type;

        private void OnAddInStack(GameObject obj)
        {
            StartCoroutine(_stackScaleCommand.Execute());
            _collectableAddOnStackCommand.Execute(obj);
        }

        private void OnRemoveInStack(GameObject obj)
        {
            _collectableRemoveOnStackCommand.Execute(obj);
        }

        private void OnChangeCollectableColor(ColorType colorType)
        {
            _type = colorType;
            _changeCollectableColorCommand.Execute(colorType);
        }

        private void Start()
        {
            Initialized();
        }

        private void Initialized()
        {
            for (int i = 0; i < 5; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Collectable);
                obj.transform.localPosition = Vector3.zero;
                obj.SetActive(true);
                OnAddInStack(obj);
                CollectableAnimSet(stackList[i], CollectableAnimationStates.Crouch);
            }
        }

        public void CollectableAnimSet(GameObject obj, CollectableAnimationStates AnimationStates)
        {
            _collectableAnimSetCommand.Execute(obj, AnimationStates);
        }

        private void OnGetStackList(GameObject _stackListObj)
        {
            _stackListObj.transform.parent = transform;
            _collectableAnimSetCommand.Execute(_stackListObj, CollectableAnimationStates.Run);
            stackList.Add(_stackListObj);
            ScoreSignals.Instance.onGetScore?.Invoke(stackList.Count);
        }

        private void OnExitColorCheckArea(ColorCheckAreaType areaType)
        {
            if (areaType == ColorCheckAreaType.Drone && stackList.Count == 0)
            {
                LevelSignals.Instance.onLevelFailed?.Invoke();
            }
        }

        private void SetCollectablesAnim()
        {
            for (int i = 0; i < stackList.Count; i++)
            {
                CollectableAnimSet(stackList[i], CollectableAnimationStates.Run);
            }
        }

        private void OnPlay()
        {
            FindPlayer();
            SetCollectablesAnim();
            ScoreSignals.Instance.onGetScore?.Invoke(stackList.Count);
        }


        private void OnReset()
        {
            stackList.Clear();
            stackList.TrimExcess();
            Initialized();
            ScoreSignals.Instance.onGetScore?.Invoke(stackList.Count);
        }
    }
}