using UnityEngine;
using Signals;
using Data.UnityObject;
using Commands;
using Datas.ValueObject;
using System.Collections.Generic;
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
            StackSignals.Instance.onTransportInStack += OnTransportInStack;
            StackSignals.Instance.onGetStackList += OnGetStackList;
            StackSignals.Instance.onChangeCollectableColor += OnChangeCollectableColor;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onPlay += OnPlay;
            StackSignals.Instance.onGetColorType += OnGetColorType;
            CoreGameSignals.Instance.onExitColorCheckArea += OnExitColorCheckArea;
        }


        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onAddInStack -= OnAddInStack;
            StackSignals.Instance.onRemoveInStack -= OnRemoveInStack;
            StackSignals.Instance.onTransportInStack -= OnTransportInStack;
            StackSignals.Instance.onGetStackList -= OnGetStackList;
            StackSignals.Instance.onChangeCollectableColor -= OnChangeCollectableColor;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            StackSignals.Instance.onGetColorType -= OnGetColorType;
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
            _collectableAnimSetCommand.Execute(obj, CollectableAnimationStates.Run);
            _collectableAddOnStackCommand.Execute(obj);
            RefreshStackCount();
        }
        private void OnRemoveInStack(GameObject obj)
        {
            _collectableRemoveOnStackCommand.Execute(obj);
            RefreshStackCount();
        }
        private void OnChangeCollectableColor(ColorType colorType)
        {
            _type = colorType;
            _changeCollectableColorCommand.Execute(colorType);
        }
        private void OnTransportInStack(GameObject _obj, Transform target)
        {
            _transportInStack.Execute(_obj, target);

        }
        private void RefreshStackCount()
        {
            ScoreSignals.Instance.onGetScore?.Invoke(stackList.Count);
        }
        private void Initialized()
        {
            for (int i = 0; i < stackList.Count; i++)
            {
                CollectableAnimSet(stackList[i]);
            }
        }
        public void CollectableAnimSet(GameObject obj)
        {
            _collectableAnimSetCommand.Execute(obj, CollectableAnimationStates.Run);
        }
        private void OnGetStackList(GameObject _stackListObj)
        {
            _stackListObj.transform.parent = transform;
            _collectableAnimSetCommand.Execute(_stackListObj, CollectableAnimationStates.Run);
            stackList.Add(_stackListObj);
            RefreshStackCount();
        }

        private void OnExitColorCheckArea(ColorCheckAreaType areaType)
        {
            if (areaType==ColorCheckAreaType.Drone && stackList.Count==0)
            {
                LevelSignals.Instance.onLevelFailed?.Invoke();

            }
        }
        private void OnPlay()
        {
            FindPlayer();
            Initialized();
            RefreshStackCount();
        }
        private void OnReset()
        {
            stackList.Clear();
            stackList.TrimExcess();
            for (int i = 0; i < 5; i++)
            {
                var obj = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Collectable);
                obj.transform.localPosition = Vector3.zero;
                obj.SetActive(true);
                OnAddInStack(obj);  
            }
            
        }
    }
}