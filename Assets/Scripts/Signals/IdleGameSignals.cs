using Extentions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Signals
{
    public class IdleGameSignals : MonoSingleton<IdleGameSignals>
    {
        public UnityAction onAreaComplete = delegate { };
        public UnityAction<GameObject> onCheckArea=delegate {  };
        public UnityAction onCostDown=delegate {  };

    }
}