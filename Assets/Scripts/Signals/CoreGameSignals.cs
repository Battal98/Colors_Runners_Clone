using Extentions;
using UnityEngine.Events;
using Enums;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction<ColorCheckAreaType> onPlayerChangeForwardSpeed = delegate { };
        public UnityAction onExitDroneArea = delegate { };
    
        public UnityAction <GameStates> onSetGameState = delegate { };
        public UnityAction <GameStates> onGetGameState = delegate { };

    }
}