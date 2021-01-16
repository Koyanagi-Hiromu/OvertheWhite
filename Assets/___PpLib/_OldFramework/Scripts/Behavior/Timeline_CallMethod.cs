using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

namespace SR
{
    [AddComponentMenu("Timeline/メソッド呼び出し")]
    [TypeInfoBox("「Add▼」から「Control Track」を選択してこのオブジェクトを貼り付けてください")]
    public class Timeline_CallMethod : SploveBehaviour, ITimeControl
    {
        public UnityEvent onTimelineStart;
        public UnityEvent onTimelineStop;

        public void OnControlTimeStart() => onTimelineStart.Invoke();
        public void OnControlTimeStop() => onTimelineStop.Invoke();
        public void SetTime(double time) { }
    }
}