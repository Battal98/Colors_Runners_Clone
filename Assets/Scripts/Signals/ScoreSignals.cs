using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> onSetScore = delegate { };

        public UnityAction<int> onGetScore = delegate { };
    }
}