using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class ColorCheckAreaSignals : MonoSingleton<ColorCheckAreaSignals>
    {
        public UnityAction onTurretActive = delegate { };
        public UnityAction onDroneActive = delegate { };
        public UnityAction<GameObject> onInteractionColorCheck = delegate { };
    } 
}
