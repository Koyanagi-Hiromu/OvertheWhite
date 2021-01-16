using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

namespace SR
{
    public static class ED_ExButton
    {
        [MenuItem("CONTEXT/Button/★ODButtonに変換する")]
        private static void Rename(MenuCommand menuCommand)
        {
            var ok = EditorUtility.DisplayDialog("★ODButtonに変換する", "戻れない処理です本当に実行するの？", "実行する", "やめる");
            if (ok)
            {
                var button = menuCommand.context as Button;
                var o = button.gameObject;
                var bce = button.onClick;
                var ue = bce as UnityEvent;

                var odbutton = o.AddComponent<ODButton>();
                odbutton.AddActivator();
                var holder = odbutton._ButtonInfoHolder;
                holder.info.OnExecute = ue;

                GameObject.DestroyImmediate(button);
            }
        }

        [MenuItem("CONTEXT/Button/★Add 効果音制御")]
        public static void AddSE(MenuCommand menuCommand)
        {
            var button = menuCommand.context as Button;
            button.gameObject.AddComponent<PointerSound>();
        }
    }
}
