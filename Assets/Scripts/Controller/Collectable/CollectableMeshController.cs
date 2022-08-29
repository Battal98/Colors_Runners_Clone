using Managers;
using UnityEngine;
using Enums;

namespace Controller
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [SerializeField] private SkinnedMeshRenderer collectableSkinMeshRenderer;
        [SerializeField] private CollectableManager manager;

        #endregion

        #endregion

        public void ChangeCollectableColor(ColorType colorType)
        {
            collectableSkinMeshRenderer.material = manager.Datas[(int)colorType].Material;
            manager.CollectableColorType = colorType;
        }
    }
}