using System;
using Sirenix.OdinInspector;

namespace SR
{
    public class ODButtonList : SploveBehaviour
    {
        [HorizontalGroup("Split", 0.5f, LabelWidth = 0.3f)]
        [BoxGroup("Split/Left", false)]
        public ODButton[] list;

        [BoxGroup("Split/Left"), Title("トグルボタン?"), ToggleLeft]
        public bool isToggle;

        [BoxGroup("Split/Left"), LabelText("デフォルトトグルId"), SuffixLabel("-1で無効")]
        public int defaultToggleId = -1;

        [BoxGroup("Split/Right", false), ToggleLeft, Title("シーン読み込みと同時にエンターする？", "[ ] フェード終わり　/ [✓] シーン読み込み終わり")]
        public bool enterOnAwake = false;

        [BoxGroup("Split/Right"), Title("Enter開始時刻(秒)", "0で終わりと同時にEnter")]
        public float delayEnterSec = 0;

        [BoxGroup("Split/Right"), Title("Enter間隔(秒)", "0でみんな同時")]
        public float delayForEachSec = 0.05f;

        [BoxGroup("Split/Left"), Button("子を自動登録", ButtonSizes.Large)]
        public void AddChildren()
        {
            list = gameObject.GetComponentsInChildren<ODButton>();
            SetOwner();
        }

        private void OnEnable()
        {
            StartEnter();
            if (isToggle && defaultToggleId > -1)
            {
                Toggle(defaultToggleId);
            }
        }

        /// <summary>
        /// フェードインが半分終わった時点
        /// </summary>
        private void StartEnter() => this.DelayCall(delayEnterSec, DelayEnter);
        private void DelayEnter()
         => list.ForEach((e, i) => this.DelayCall(i * delayForEachSec, e.Enter));

        public void Untoggle()
        {
            list.ForEach(e =>
            {
                e.isToggleON = false;
                e.Unselect();
            });
        }

        public void Toggle_AnimOnly(int id)
        {
            list.ForEach((e, i) =>
            {
                if (i == id)
                {
                    e.isToggleON = true;
                    e.Select_AnimOnly();
                }
                else
                {
                    e.isToggleON = false;
                    e.Unselect();
                }
            });
        }

        public void Toggle(int id)
        {
            list.ForEach((e, i) =>
            {
                if (i == id)
                {
                    e.isToggleON = true;
                    e.Select();
                }
                else
                {
                    e.isToggleON = false;
                    e.Unselect();
                }
            });
        }

        public void Toggle(ODButton select)
        {
            list.ForEach((e) =>
            {
                if (e == select)
                {
                    e.isToggleON = true;
                    e.Select();
                }
                else
                {
                    e.isToggleON = false;
                    e.Unselect();
                }
            });
        }

        public void Toggle_LongHold(ODButton select)
        {
            list.ForEach((e) =>
            {
                if (e == select)
                {
                    e.isToggleON = true;
                    e.Select_LongHold();
                }
                else
                {
                    e.isToggleON = false;
                    e.Unselect();
                }
            });
        }

        [Button("登録されてるボタン達の親を設定する")]
        void SetOwner()
        {
            list.ForEach(e =>
            {
                e.owner = this;
            });
        }
    }
}
