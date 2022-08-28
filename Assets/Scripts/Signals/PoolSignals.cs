using System;
using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<PoolType, GameObject> onGetPoolObject = delegate { return null; };
        public UnityAction<GameObject, PoolType> onSendPool = delegate { };
    }
}