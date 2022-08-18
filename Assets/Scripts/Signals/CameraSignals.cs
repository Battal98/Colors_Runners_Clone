using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public UnityAction<CameraStatesType> onSetCameraState = delegate { };
        public UnityAction<Transform> onSetCameraTarget = delegate { };
    }
}