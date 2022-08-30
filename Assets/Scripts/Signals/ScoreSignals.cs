using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> onSetPlayerScore = delegate { };
        public UnityAction<int> onGetPlayerScore = delegate { };
        
        public Func<int> onGetIdleScore= delegate { return 0;};
        public UnityAction<int> onSetIdleScore=delegate {  };

    }
}