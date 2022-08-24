using System.Collections.Generic;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using UnityEngine;

namespace Managers
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorType ColorType;
        [HideInInspector] public List<ColorData> ColorDatas;

        #endregion

        #region Serializable Variables

        [SerializeField] private GateMeshController gateMeshController;

        #endregion

        #endregion

        private void Awake()
        {
            ColorDatas = GetColorData();
            SetGateColor();
        }

        private List<ColorData> GetColorData()
        {
            return Resources.Load<CD_Color>("Data/CD_Color").Data;
        }

        private void SetGateColor()
        {
            gateMeshController.ChangeGateColor();
        }
    }
}