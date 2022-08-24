using UnityEngine;

namespace Managers
{
    public class IdleManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject cityHolder;

        #endregion

        #region Private Variables

        private int _cityLevel;
        #endregion

        #endregion

        private void Awake()
        {
            
        }

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
        
        }

        private void UnSubscribeEvent()
        {

        }
        private void OnDisable()
        {
            UnSubscribeEvent();
        }

        #endregion

        private void Start()
        {
            OnInitializeLevel();
        }

        private void OnInitializeLevel()
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/CityPrefabs/City {_cityLevel}"),
                cityHolder.transform);
        }
    }
}