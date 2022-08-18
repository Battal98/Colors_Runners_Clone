using Managers;
using UnityEngine;

namespace Controller
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        [SerializeField] private SkinnedMeshRenderer collectableSkinMeshRenderer;
        [SerializeField] private CollectableManager manager;

        #endregion

        #endregion


        public void CollectableMaterial(Material colorType)
        {
            manager.CollectableMaterialData = colorType;
            collectableSkinMeshRenderer.material = colorType;
        }
    }
}