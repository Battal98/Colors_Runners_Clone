using UnityEngine;
using Controllers;
using Signals;
using Data.UnityObject;
using Commands;
using Datas.ValueObject;
using System.Collections.Generic;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Veriables

        #region Public Veriables

        [Header("Data")] public StackData StackData;   
        
        #endregion

        #region Serilazible Veriables

        [SerializeField] private StackDecreaseController stackDecreaseController;
        [SerializeField] private GameObject collectorMeshRenderer;
        [SerializeField] private List<GameObject> stackList = new List<GameObject>();

        #endregion

        #region Private Veriables

        private StackIncreaseCommand _stackIncreaseCommand;        
        private StackLerpMovementCommand _stackLerpMovementCommand;
        private StackScaleCommand _stackScaleCommand;
        private Transform _playerManager;
        private float _stackScore;

        #endregion

        #endregion

        private void Awake()
        {
            StackData = GetStackData();
            stackDecreaseController = GetComponent<StackDecreaseController>();
            _stackIncreaseCommand = new StackIncreaseCommand();
            _stackLerpMovementCommand = new StackLerpMovementCommand();
            _stackScaleCommand = new StackScaleCommand();

        }

        private void Start()
        {
            if (!_playerManager)
            {
                Debug.Log(_playerManager);
                _playerManager = FindObjectOfType<PlayerManager>().transform;
            }
        }

        #region Event Subscription


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
            
            StackSignals.Instance.onDecreaseStack += OnStackHitTheObstacleDecrease;
            
            StackSignals.Instance.onInitStackIncrease += OnInitStackIncrease;
           // StackSignals.Instance.onRandomThrowCollectable += OnRandomThrowCollectable;

            CoreGameSignals.Instance.onReset += OnReset;
        }

       

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            
            StackSignals.Instance.onDecreaseStack -= OnStackHitTheObstacleDecrease;
            
            StackSignals.Instance.onInitStackIncrease -= OnInitStackIncrease;
            //StackSignals.Instance.onRandomThrowCollectable -= OnRandomThrowCollectable;

            CoreGameSignals.Instance.onReset -= OnReset;
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

        #region Stack Increase Decrease Jobs

        #region Increase Jobs
        private void OnInitStackIncrease()
        {
            OnIncreaseStack(Instantiate(StackData.CollectableObject));
        }
        public void OnIncreaseStack(GameObject _obj)
        { 
           // if (collectorMeshRenderer.GetComponent<Material>().color == _obj.gameObject.GetComponent<Material>().color)
            //{
                if (!_playerManager)
                {
                    _playerManager = FindObjectOfType<PlayerManager>().transform;
                }
                StartCoroutine(_stackScaleCommand.ScaleSizeUpAndDown(stackList,StackData.StackMaxScaleValue, StackData.StackScaleDelay, StackData.StackTaskDelay));
                if (stackList.Count == 0)
                {
                    var pos = new Vector3(0, _obj.transform.position.y, 1f);
                    _stackIncreaseCommand.IncreaseFunc(_obj, this.gameObject, pos, stackList);
                }
                else
                {
                    var pos = new Vector3(0, _obj.transform.position.y, stackList[stackList.Count - 1].transform.localPosition.z + 1f);
                    _stackIncreaseCommand.IncreaseFunc(_obj, this.gameObject, pos, stackList);
                }
           //}
        }
        #endregion

        #region Decrease Jobs
        public void OnStackHitTheObstacleDecrease(GameObject _obj)
        {
            stackDecreaseController.StackHitTheObstacleDecrease(_obj, stackList);
           
        }
        public void OnStackGeneralDecrease(GameObject _obj, Transform _targetParent)
        {
            stackDecreaseController.StackGeneralDecrease(_obj, stackList, _targetParent);
        }
        #endregion
        #endregion

        private void Update()
        {
            transform.position = new Vector3(0, 0,_playerManager.position.z-StackData.StackOffset);
            _stackLerpMovementCommand.StackLerpMovement(ref stackList, _playerManager,ref StackData.StackLerpXDelay,ref StackData.StackLerpYDelay,ref StackData.StackOffset);

        }

        private void OnReset()
        {
            stackList.Clear();
            stackList.TrimExcess();
        }

    }
}
