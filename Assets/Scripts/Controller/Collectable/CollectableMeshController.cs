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

        private SkinnedMeshRenderer _collectableSkinMeshRenderer;

        #endregion

        #endregion

        private void Awake()
        {
            _collectableSkinMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        public void CollectableMaterial(ColorType colorType)
        {
            _collectableSkinMeshRenderer.material = Resources.Load<Material>($"Materials/{colorType}Material");
        }
    }
}