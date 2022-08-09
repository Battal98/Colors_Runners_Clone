using System.Collections;
using System.Collections.Generic;
using Controller.Stack;
using Data.UnityObject;
using DG.Tweening;
using Signals;
using UnityEngine;
using Datas.ValueObject;
using Enums;
using Player.Controllers;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
        public CollectableData CollectableData;
        public List<GameObject> Collected = new List<GameObject>();
        public GameObject TempHolder;
        
        #endregion
    
        #region Serialized Variables

        [SerializeField] private GameObject collectorMeshRenderer;
        
        
        
        #endregion
    
        #region Private Variables

        #endregion
    
        #endregion
        
        #region Event Subscription 
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
            // StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
            // StackSignals.Instance.onStackMove += OnStackMove;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            // StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
            // StackSignals.Instance.onStackMove -= OnStackMove;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Awake()
        {
            CollectableData = GetCollectableData();
        }
        private CollectableData GetCollectableData()
        {
            return Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData;
        }
        private void OnIncreaseStack(GameObject other)
        {
            if (collectorMeshRenderer.GetComponent<Renderer>().material.color == other.gameObject.GetComponent<Renderer>().material.color)
            {
                AddOnStack(other);
                //StartCoroutine(CollectableScaleUp());
            }
        }

        private void AddOnStack(GameObject other)
        {
            other.transform.parent = transform ;
            other.gameObject.transform.localPosition = new Vector3(0f, 0f, Collected[Collected.Count - 1].transform.localPosition.z - 0.5f);
            Collected.Add(other.gameObject);
            
            
        }
        // public IEnumerator CollectableScaleUp()
        // {
        //     for (int i = Collected.Count -1; i >= 0; i--)
        //     {
        //         int index = i;
        //         Vector3 scale = Vector3.one * 1.5f;
        //         Collected[index].transform.DOScale(scale, 0.2f).SetEase(Ease.Flash);
        //         Collected[index].transform.DOScale(Vector3.one, 0.2f).SetDelay(0.2f).SetEase(Ease.Flash);
        //         yield return new WaitForSeconds(0.05f);
        //     }
        //     
        // }
    }
}