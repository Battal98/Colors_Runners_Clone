using System;
using Enums;
using UnityEngine;

namespace Controller.Collectable
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables

        #endregion
    
        #region Serialized Variables

        #endregion
    
        #region Private Variables
        [SerializeField]
        private SkinnedMeshRenderer _collectableSkinMeshRenderer;

        #endregion

        #endregion

        private void Awake()
        {
            
        }

        public void CollectableMaterial(Material colorType)
        {
            _collectableSkinMeshRenderer.material = colorType;
        }
    }
}