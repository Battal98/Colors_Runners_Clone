using UnityEngine;
using UnityEngine.Events;
using Extentions;
using System.Collections.Generic;
using Enums;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onAddInStack = delegate{ };
        public UnityAction<GameObject> onRemoveInStack = delegate{ };
        public UnityAction<GameObject> onTransportInStack = delegate{ };
        public UnityAction<Vector2> onStackMove = delegate { };
        public UnityAction onInitStackIncrease=delegate {  };
        public UnityAction onStackJumpPlatform = delegate {  };
        public UnityAction<List<GameObject>> onSendStackList = delegate { };
        public UnityAction<GameObject,CollectableAnimationStates> onSetCollectableAnimState = delegate { };
    }
}