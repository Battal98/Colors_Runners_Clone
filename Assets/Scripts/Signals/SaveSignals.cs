using System;
using Extentions;
using UnityEngine;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public Func<int> onGetLevel= delegate { return 0;};
    }
}