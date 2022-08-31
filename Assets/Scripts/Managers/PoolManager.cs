using System.Threading.Tasks;
using Commands.Pool;
using Data.UnityObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;
        [SerializeField] private Transform poolManagerG;

        #endregion

        #region Private Variables

        private CD_PoolGenerator _cdPoolGenerator;
        private GameObject _emptyGameObject;
        private PoolGenerateCommand _poolGenerateCommand;
        private RestartPoolCommand _restartPoolCommand;

        #endregion

        #endregion


        private void Awake()
        {
            GetReferences();
            Init();
            StartPool();
        }

        private void GetReferences()
        {
            _cdPoolGenerator = GetPoolData();
        }

        private void Init()
        {
            _poolGenerateCommand =
                new PoolGenerateCommand(ref _cdPoolGenerator, ref poolManagerG, ref _emptyGameObject);
            _restartPoolCommand = new RestartPoolCommand(ref _cdPoolGenerator, ref poolManagerG, ref levelHolder);
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            CoreGameSignals.Instance.onReset += OnRestart;
            PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
            PoolSignals.Instance.onSendPool += OnSendPool;
        }

        private void UnSubscribeEvent()
        {
            CoreGameSignals.Instance.onReset -= OnRestart;
            PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
            PoolSignals.Instance.onSendPool -= OnSendPool;
        }

        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion


        private void StartPool()
        {
            _poolGenerateCommand.Execute();
        }

        private CD_PoolGenerator GetPoolData()
        {
            return Resources.Load<CD_PoolGenerator>("Data/CD_PoolGenerator");
        }

        private void RestartPool()
        {
            _restartPoolCommand.Execute();
        }

        private GameObject OnGetPoolObject(PoolType poolType)
        {
            var parent = transform.GetChild((int)poolType);
            var obj = parent.childCount != 0
                ? parent.transform.GetChild(0).gameObject
                : Instantiate(_cdPoolGenerator.PoolObjectList[(int)poolType].Pref,Vector3.zero, Quaternion.identity, parent);

            if (StackSignals.Instance.onGetColorType() == ColorType.RainBow)
                StackSignals.Instance.OnSetColorType?.Invoke(ColorType.Blue);

            obj.GetComponent<CollectableManager>().CollectableColorChange(StackSignals.Instance.onGetColorType());
            return obj;
        }

        private void OnSendPool(GameObject CollectableObject, PoolType poolType)
        {
            CollectableObject.transform.parent = transform.GetChild((int)poolType);
            CollectableObject.GetComponentInChildren<Collider>().enabled = true;
            CollectableObject.SetActive(false);
            CollectableObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            CollectableObject.transform.localPosition = new Vector3(0f, 0f, 0);
        }

        private void OnRestart()
        {
            RestartPool();
        }
    }
}