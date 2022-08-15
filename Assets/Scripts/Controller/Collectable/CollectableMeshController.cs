using System;
using Enums;
using Managers;
using UnityEngine;

namespace Controller.Collectable
{
    public class CollectableMeshController 
    {
        #region Self Variables
        #region Public Variables
        #endregion
        #region Serialized Variables
        #endregion
        #region Private Variables
        private SkinnedMeshRenderer _collectableSkinMeshRenderer;
        private CollectableManager _manager;
        #endregion
        #endregion

        public CollectableMeshController(ref SkinnedMeshRenderer collectableSkinMeshRenderer,ref CollectableManager manager)
        {

            _collectableSkinMeshRenderer = collectableSkinMeshRenderer;
            _manager = manager;
        }
        

        public void CollectableMaterial(Material colorType)
        {
            _manager.CollectableMaterial = colorType;
            _collectableSkinMeshRenderer.material = colorType;
        }
    }
}