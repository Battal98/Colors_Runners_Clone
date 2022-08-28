using Extentions;
using UnityEngine.Events;
using Enums;
using UnityEngine;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction<ColorCheckAreaType> onExitColorCheckArea = delegate { };
        public UnityAction<GameStates> onSetGameState = delegate { };
        public UnityAction<GameStates> onGetGameState = delegate { };
        public UnityAction<Transform> onSetCameraTarget = delegate { };
    }
}