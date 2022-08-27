using System;
using Datas.ValueObject;
using Extentions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Signals
{
    public class IdleGameSignals : MonoSingleton<IdleGameSignals>
    {
        public UnityAction onAreaComplete = delegate { };
        public UnityAction<GameObject> onCheckArea = delegate { };
        public UnityAction onCostDown = delegate { };
        public UnityAction onRefresthAreaData = delegate { };
        public UnityAction<int, AreaData> onSetAreaData = delegate { };
        public Func<int, AreaData> onGetAreaData = delegate { return default; };
        public UnityAction onPrepareAreaWithSave=delegate {  };
    }
}