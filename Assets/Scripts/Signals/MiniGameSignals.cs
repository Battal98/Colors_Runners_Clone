using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class MiniGameSignals : MonoSingleton<MiniGameSignals>
    {
        public UnityAction onEnterInDroneArea=delegate {  };
        public UnityAction onEnterInTurretArea=delegate {  };

    }
}