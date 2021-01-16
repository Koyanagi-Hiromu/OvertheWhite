using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    public class EnterAnimatorList : SploveBehaviour
    {
        [HorizontalGroup("Split", 0.5f, LabelWidth = 0.3f)]
        [BoxGroup("Split/Left", false)]
        public Animator[] list;

        [BoxGroup("Split/Right", false), ToggleLeft, Title("シーン読み込みと同時にする？", "[ ] フェード終わり　/ [✓] シーン読み込み終わり")]
        public bool enterOnAwake = false;

        [BoxGroup("Split/Right"), Title("エンター開始(秒)", "0で終わりと同時にエンター")]
        public float delayEnterSec = 0;

        [BoxGroup("Split/Right"), Title("エンター間隔(秒)", "0でみんな同時")]
        public float delayForEachSec = 0.05f;

        [BoxGroup("Split/Left"), Button("子を自動登録", ButtonSizes.Large)]
        public void AddChildren()
        {
            list = gameObject.GetComponentsInChildren<Animator>();
        }

        private void Awake()
        {
            // if (!enterOnAwake && FadeManager.Ins && FadeManager.Ins.isLoading)
            //     FadeManager.Ins.onButtonEnter += StartEnter;
            // else
                StartEnter();
        }

        private void StartEnter() => this.DelayCall(delayEnterSec, DelayEnter);

        private void DelayEnter()
         => list.ForEach((e, i) => this.DelayCall(i * delayForEachSec, () => e.SetTrigger("Enter")));
    }
}
