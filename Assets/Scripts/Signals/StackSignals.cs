using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onIncreaseStack = delegate{ };
        public UnityAction<GameObject> onDecreaseStack = delegate{ };
        public UnityAction<Vector2> onStackMove = delegate { };
    }
}