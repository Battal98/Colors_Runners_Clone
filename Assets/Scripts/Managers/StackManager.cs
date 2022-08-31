using System.Collections.Generic;
using Commands;
using Data.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public StackData StackData;

        #endregion

        #region Serilazible Variables

        [SerializeField] private StackManager stackManager;

        #endregion

        #region Private Variables

        private CollectableAddOnStackCommand _collectableAddOnStackCommand;
        private StackLerpMovementCommand _stackLerpMovementCommand;
        private StackShackAnimCommand _stackShackAnimCommand;
        private CollectableRemoveOnStackCommand _collectableRemoveOnStackCommand;
        private TransportInStack _transportInStack;
        private CollectableAnimSetCommand _collectableAnimSetCommand;
        private ChangeCollectableColorCommand _changeCollectableColorCommand;
        private RandomKillInStackCommand _randomKillInStackCommand;
        private StackItemsCombineCommand _stackItemsCombineCommand;
        private StackMultiplierCommand _stackMultiplierCommand;
        private Transform _playerManager;
        [ShowInInspector] private ColorType _type;
        [ShowInInspector] private List<GameObject> _stackList = new List<GameObject>();
        [ShowInInspector] private List<GameObject> _tempHolder = new List<GameObject>();

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            Init();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onAddInStack += OnAddInStack;
            StackSignals.Instance.onRemoveInStack += _collectableRemoveOnStackCommand.Execute;
            StackSignals.Instance.onTransportInStack += _transportInStack.Execute;
            StackSignals.Instance.onGetStackList += OnGetStackList;
            StackSignals.Instance.onChangeCollectableColor += OnChangeCollectableColor;
            StackSignals.Instance.onKillRandomInStack += _randomKillInStackCommand.Execute;
            StackSignals.Instance.onGetColorType += OnGetColorType;
            StackSignals.Instance.OnSetColorType += OnSetColorType;
            StackSignals.Instance.onEnterMultiplier += _stackMultiplierCommand.Execute;

            CoreGameSignals.Instance.onEnterFinish += OnEnterFinish;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onExitColorCheckArea += OnExitColorCheckArea;
        }


        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddInStack -= OnAddInStack;
            StackSignals.Instance.onRemoveInStack -= _collectableRemoveOnStackCommand.Execute;
            StackSignals.Instance.onTransportInStack -= _transportInStack.Execute;
            StackSignals.Instance.onGetStackList -= OnGetStackList;
            StackSignals.Instance.onChangeCollectableColor -= OnChangeCollectableColor;
            StackSignals.Instance.onKillRandomInStack -= _randomKillInStackCommand.Execute;
            StackSignals.Instance.onGetColorType -= OnGetColorType;
            StackSignals.Instance.OnSetColorType -= OnSetColorType;
            StackSignals.Instance.onEnterMultiplier += _stackMultiplierCommand.Execute;

            CoreGameSignals.Instance.onEnterFinish -= OnEnterFinish;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onExitColorCheckArea -= OnExitColorCheckArea;
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


        private void GetReferences()
        {
            StackData = GetStackData();
        }

        private void Init()
        {
            _collectableAddOnStackCommand =
                new CollectableAddOnStackCommand(ref stackManager, ref _stackList, ref StackData);
            _stackLerpMovementCommand = new StackLerpMovementCommand(ref _stackList, ref StackData);
            _stackShackAnimCommand = new StackShackAnimCommand(ref _stackList, ref StackData);
            _collectableRemoveOnStackCommand = new CollectableRemoveOnStackCommand(ref _stackList, ref stackManager,
                ref StackData);
            _transportInStack = new TransportInStack(ref _stackList, ref stackManager, ref StackData);
            _collectableAnimSetCommand = new CollectableAnimSetCommand();
            _changeCollectableColorCommand = new ChangeCollectableColorCommand(ref _stackList);
            _randomKillInStackCommand = new RandomKillInStackCommand(ref stackManager, ref _stackList,
                ref StackData);
            _stackItemsCombineCommand =
                new StackItemsCombineCommand(ref _stackList, ref StackData, ref stackManager, ref _tempHolder);
            _stackMultiplierCommand = new StackMultiplierCommand(ref _stackList, ref stackManager);
        }

        private ColorType OnGetColorType()
        {
            return _type;
        }

        private void OnSetColorType(ColorType colorType)
        {
            _type = colorType;
        }

        private void Update()
        {
            if (!_playerManager)
                return;
            _stackLerpMovementCommand.Execute(ref _playerManager);
        }

        public void AddInStack(GameObject obj)
        {
            _collectableAddOnStackCommand.Execute(obj);
        }

        public void CollectableAnimSet(GameObject obj, CollectableAnimationStates AnimationStates)
        {
            _collectableAnimSetCommand.Execute(obj, AnimationStates);
        }

        private void OnEnterFinish()
        {
            OnChangeCollectableColor(ColorType.RainBow);
            _stackItemsCombineCommand.Execute();
        }


        private void SetAllCollectableAnim(CollectableAnimationStates states)
        {
            for (var i = 0; i < _stackList.Count; i++) CollectableAnimSet(_stackList[i], states);
        }

        private void FindPlayer()
        {
            if (!_playerManager) _playerManager = FindObjectOfType<PlayerManager>().transform;
        }


        private void OnAddInStack(GameObject obj)
        {
            StartCoroutine(_stackShackAnimCommand.Execute());
            CollectableAnimSet(obj, CollectableAnimationStates.Run);
            AddInStack(obj);
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
            for (var i = 0; i < 60; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Collectable);
                obj.SetActive(true);
                _collectableAddOnStackCommand.Execute(obj);
            }

            SetAllCollectableAnim(CollectableAnimationStates.Crouch);
        }


        private void OnGetStackList(GameObject _stackListObj)
        {
            _stackListObj.transform.parent = transform;
            _collectableAnimSetCommand.Execute(_stackListObj, CollectableAnimationStates.Run);
            _stackList.Add(_stackListObj);
            ScoreSignals.Instance.onGetPlayerScore?.Invoke(_stackList.Count);
        }

        private void OnExitColorCheckArea(ColorCheckAreaType areaType)
        {
            if (areaType == ColorCheckAreaType.Drone && _stackList.Count == 0)
                LevelSignals.Instance.onLevelFailed?.Invoke();
        }


        private void ClearStackManager()
        {
            var _items = stackManager.transform.childCount;
            for (var i = 0; i < _items; i++)
                PoolSignals.Instance.onSendPool?.Invoke(stackManager.transform.GetChild(0).gameObject,
                    PoolType.Collectable);
        }

        private void OnPlay()
        {
            FindPlayer();
            SetAllCollectableAnim(CollectableAnimationStates.Run);
            ScoreSignals.Instance.onGetPlayerScore?.Invoke(_stackList.Count);
        }


        private void OnReset()
        {
            ClearStackManager();
            _stackList.Clear();
            _stackList.TrimExcess();
            Initialized();
            ScoreSignals.Instance.onGetPlayerScore?.Invoke(_stackList.Count);
        }
    }
}