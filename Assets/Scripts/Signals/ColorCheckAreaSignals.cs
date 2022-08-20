using Enums;
using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class ColorCheckAreaSignals : MonoSingleton<ColorCheckAreaSignals>
    {
    
        public UnityAction<GameObject> onCheckAreaControl = delegate { };
    }
}