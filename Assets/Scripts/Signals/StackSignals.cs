using UnityEngine;
using UnityEngine.Events;
using Extentions;
using System.Collections.Generic;
using Enums;
using System;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onAddInStack = delegate { };
        public UnityAction<GameObject> onRemoveInStack = delegate { };
        public UnityAction<GameObject, Transform> onTransportInStack = delegate { };
        public UnityAction<GameObject> onGetStackList = delegate { };
        public UnityAction<ColorType> onChangeCollectableColor = delegate { };
        public UnityAction onStackTransferComplete = delegate { };
        public UnityAction onKillRandomInStack = delegate { };
        public UnityAction<Transform> onCollectablesThrow = delegate { };
        public UnityAction onEnterMultiplier = delegate { };
        public UnityAction<float> onScaleSet=delegate {  };

        public Func<ColorType> onGetColorType = delegate { return 0; };
    }
}