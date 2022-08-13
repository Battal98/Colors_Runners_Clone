using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class IdleGameSignals : MonoSingleton<IdleGameSignals>
    {
        public UnityAction<int> onAreaComplete = delegate { };
    }
}