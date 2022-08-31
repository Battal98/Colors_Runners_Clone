using System;
using Datas.ValueObject;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class IdleGameSignals : MonoSingleton<IdleGameSignals>
    {
        public UnityAction onCostDown = delegate { };
        public UnityAction onAreaComplete = delegate { };
        public UnityAction onCityComplete = delegate { };
        public UnityAction onRefreshAreaData = delegate { };
        public UnityAction onPrepareAreaWithSave = delegate { };
        public UnityAction<GameObject> onCheckArea = delegate { };
        public UnityAction<int, AreaData> onSetAreaData = delegate { };
        
        public Func<int, AreaData> onGetAreaData = delegate { return default; };
    }
}