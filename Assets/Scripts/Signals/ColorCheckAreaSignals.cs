using Enums;
using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class ColorCheckAreaSignals : MonoSingleton<ColorCheckAreaSignals>
    {
        public UnityAction<float> onSetCollectableOutline = delegate { };
        public UnityAction<ColorCheckAreaType> onChangeJobsOnColorArea=delegate {  };
        public UnityAction<GameObject> onCheckAreaControl = delegate { };
        public UnityAction<bool> onTurretIsActive=delegate {  };
    }
}