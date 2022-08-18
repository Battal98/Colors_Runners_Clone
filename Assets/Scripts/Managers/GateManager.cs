using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Managers;
using Controllers;
using Data.ValueObject;
using Data.UnityObject;
using System;

namespace Managers
{
    public class GateManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorType ColorType;
        [HideInInspector]
        public List<ColorData> ColorDatas;

        #endregion

        #region Serializable Variables

        [SerializeField]
        private GateMeshController gateMeshController;

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
