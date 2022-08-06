using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction onSaveData=delegate {  };
        public Func<int> onGetLevel= delegate { return 0;};
    }
}