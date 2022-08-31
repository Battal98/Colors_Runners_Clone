using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onInputTaken = delegate { };
        public UnityAction onInputReleased = delegate { };
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction<InputParams> onInputDragged = delegate { };
    }
}