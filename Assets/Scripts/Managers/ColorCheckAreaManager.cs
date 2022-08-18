using System.Collections.Generic;
using Commands.ColorCheckArea;
using Controllers;
using Enums;
using UnityEngine;

namespace Managers
{
    public class ColorCheckAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorCheckAreaType AreaType=ColorCheckAreaType.Drone;
        #endregion

        #region Serialized Variables

        [SerializeField] private ColorCheckAreaMeshController colorCheckAreaMeshController;
        [SerializeField] private ColorCheckAreaPhysicController colorCheckAreaPhysicController;

        #endregion

        #region Private Variables

        private OutLineChangeCommand _outLineChangeCommand;
        private CollectablePositionSetCommand _collectablePositionSetCommand;

        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _outLineChangeCommand = new OutLineChangeCommand();
            _collectablePositionSetCommand = new CollectablePositionSetCommand();
        }

        public void SetOutline(List<GameObject> stack, float endValue)
        {
            _outLineChangeCommand.Execute(stack, endValue);
        }

        public void MoveCollectablesToArea(GameObject other, Transform _colHolder)
        {
            _collectablePositionSetCommand.Execute(other, _colHolder);
        }

        public void SetTargetForTurrets()
        {
            
        }
    }
}