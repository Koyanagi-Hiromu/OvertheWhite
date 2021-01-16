using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PPD
{
    public class HorizontalLoop_ScrollRectContent : PPD_MonoBehaviour
    {
        public const float PagingThreshold = 0.20f;
        public const float DampRate = 0.13f;
        public static readonly Vector3 FarAwary = new Vector3(1000000, 1000000, 1000000);
        [Serializable] public class OnChangeId : UnityEvent<int> { }
        enum Mode { Centerized, Flicking, Inertia, Centerizing, }

        public ScrollRect owner;
        public RectTransform viewPort;
        public RectTransform rectTransform;
        public GameObject prefabScrollElement;
        public OnChangeId onChangeId;

        [SerializeField, Min(0)] float inertiaEndSpeedThreshold = 2.5f;
        float flickedDetectionSpeedThreshold;
        IScrollElement[] elements = new IScrollElement[3];
        int _currentIndex;
        int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (value <= min)
                {
                    _currentIndex = min;
                    hPosition = Mathf.Max(hPosition, _currentIndex - 0.5f);
                }
                else if (value >= max)
                {
                    _currentIndex = max;
                    hPosition = Mathf.Min(hPosition, _currentIndex + 0.5f);
                }
                else
                {
                    _currentIndex = value;
                }
            }
        }
        bool isInited;
        int lastIndex;
        int flickStartIndex;
        Mode mode;
        float width;
        int min = 1, max = 10;

        float hPosition
        {
            get => owner.horizontalNormalizedPosition - 0.5f;
            set => owner.horizontalNormalizedPosition = value + 0.5f;
        }

        public void Init(int index, int min, int max)
        {
            this.min = Mathf.Min(min, max);
            this.max = Mathf.Max(min, max, index);
            this.CurrentIndex = index;
            this.hPosition = CurrentIndex;

            TryInit();
            FRefreshLooks();
        }

        void TryInit()
        {
            if (isInited) return;

            isInited = true;
            flickedDetectionSpeedThreshold = inertiaEndSpeedThreshold * 0.5f;

            var prefab = prefabScrollElement.GetComponent<IScrollElement>();

            var sizeDelta = prefab.rectTransform.sizeDelta;
            this.width = prefab.rectTransform.sizeDelta.x;

            //viewport Size
            {
                var vpSizeDelta = viewPort.sizeDelta;
                vpSizeDelta.x = sizeDelta.x;
                viewPort.sizeDelta = vpSizeDelta;
            }
            //content Size
            {
                sizeDelta.x *= 2;
                rectTransform.sizeDelta = sizeDelta;
            }
            mode = Mode.Centerized;
            this.elements[0] = prefab.Inst(rectTransform);
            this.elements[1] = prefab.Inst(rectTransform);
            this.elements[2] = prefab.Inst(rectTransform);

            hPosition = CurrentIndex;
        }

        public void TryRefreshLooks()
        {
            if (lastIndex != CurrentIndex)
            {
                FRefreshLooks();
            }
        }

        private void FRefreshLooks()
        {
            lastIndex = CurrentIndex;

            var p = new Vector2(width * CurrentIndex, 0);
            var L_Id = GetIndex(CurrentIndex - 1);
            var C_id = GetIndex(CurrentIndex);
            var R_Id = GetIndex(CurrentIndex + 1);

            var speed = owner.velocity.x;
            // SLog.Check.Info($"speed:{speed}, currentIndex:{currentIndex}, hPosition:{hPosition:F2}");

            {
                p.x -= width;
                if (CurrentIndex <= min)
                {
                    this.elements[L_Id].rectTransform.localPosition = FarAwary;
                }
                else
                {
                    this.elements[L_Id].rectTransform.localPosition = p;
                    if (this.elements[L_Id].id != CurrentIndex - 1)
                    {
                        this.elements[L_Id].id = CurrentIndex - 1;
                    }
                }

                p.x += width;
                this.elements[C_id].rectTransform.localPosition = p;
                if (this.elements[C_id].id != CurrentIndex)
                {
                    this.elements[C_id].id = CurrentIndex;
                }

                p.x += width;
                if (CurrentIndex >= max)
                {
                    this.elements[R_Id].rectTransform.localPosition = FarAwary;
                }
                else
                {
                    this.elements[R_Id].rectTransform.localPosition = p;
                    if (this.elements[R_Id].id != CurrentIndex + 1)
                    {
                        this.elements[R_Id].id = CurrentIndex + 1;
                    }
                }
            }

            onChangeId.Invoke(CurrentIndex);
        }

        int GetIndex(int x)
        {
            var id = x % 3;
            if (id < 0)
            {
                id += 3;
            }
            return id;
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (mode != Mode.Flicking)
                {
                    mode = Mode.Flicking;
                    flickStartIndex = CurrentIndex;
                }
            }

            switch (mode)
            {
                case Mode.Centerized:
                    break;
                case Mode.Flicking:
                    if (Input.GetMouseButton(0))
                    {
                        var prevIndex = CurrentIndex;
                        CurrentIndex = Mathf.RoundToInt(hPosition);
                    }
                    else
                    {
                        CurrentIndex = Mathf.RoundToInt(hPosition);
                        mode = Mode.Inertia;
                    }
                    TryRefreshLooks();
                    break;
                case Mode.Inertia:
                    CurrentIndex = Mathf.RoundToInt(hPosition);
                    var speed = owner.velocity.x.Abs() / width;
                    if (speed < inertiaEndSpeedThreshold)
                    {
                        var diff = hPosition - CurrentIndex;

                        if (speed < flickedDetectionSpeedThreshold)
                        {
                            if (CurrentIndex == flickStartIndex && diff.Abs() > PagingThreshold)
                            {
                                CurrentIndex += diff < 0 ? -1 : +1;
                            }
                        }
                        else
                        {
                            if (owner.velocity.x < 0 != diff < 0)
                            {
                                CurrentIndex += diff < 0 ? -1 : +1;
                            }
                        }
                        mode = Mode.Centerizing;
                    }
                    TryRefreshLooks();
                    break;
                case Mode.Centerizing:
                    var curDiff = hPosition - CurrentIndex;

                    var dampedValue = curDiff * (1 - DampRate);
                    var delta = dampedValue - curDiff;
                    var maxDelta = inertiaEndSpeedThreshold * Time.deltaTime;
                    delta = Mathf.Clamp(delta, -maxDelta, maxDelta);

                    var nextDiff = curDiff + delta;
                    if (nextDiff.Abs() > 0.01f)
                    {
                        hPosition = CurrentIndex + nextDiff;
                    }
                    else
                    {
                        nextDiff = 0;
                        hPosition = CurrentIndex;

                        mode = Mode.Centerized;
                        TryRefreshLooks();
                    }
                    break;
            }
        }
    }
}
