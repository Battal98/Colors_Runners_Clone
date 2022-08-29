using Managers;
using UnityEngine;

namespace Controllers
{
    public class GateMeshController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer gateMeshRenderer;
        [SerializeField] private GateManager manager;

        public void ChangeGateColor()
        {
            gateMeshRenderer.material.color = manager.ColorDatas[(int)manager.ColorType].Material.color;
        }
    }
}