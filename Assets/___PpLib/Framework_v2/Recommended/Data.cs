using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    public class Data : _Data_ForReadability
    {
        [FoldoutGroup("HCGテンプレ", order: 1)]
        [BoxGroup("HCGテンプレ/重力", order: -1), ShowInInspector]
        public float Gravity
        {
            get => Physics.gravity.y;
            set
            {
                Physics.gravity = new Vector3(Physics.gravity.x, value, Physics.gravity.z);
                Physics2D.gravity = new Vector2(Physics2D.gravity.x, value);
            }
        }

        [BoxGroup("HCGテンプレ/シーン読み込み"), LabelText("シェイク秒")] public float nextSceneLoadHandler_shakeSec = 0.5f;
        [BoxGroup("HCGテンプレ/シーン読み込み"), LabelText("フェードアウト秒")] public float nextSceneLoadHandler_fadeoutSec = 0.15f;
        [BoxGroup("HCGテンプレ/シーン読み込み"), LabelText("フェードイン秒")] public float nextSceneLoadHandler_fadeinSec = 0.4f;
        [BoxGroup("HCGテンプレ/シーン読み込み"), LabelText("フェード色、開始時")] public Color nextSceneLoadHandler_defaultColor = new Color(0, 0, 0, 0);
        [BoxGroup("HCGテンプレ/シーン読み込み"), LabelText("フェード色、頂点時")] public Color nextSceneLoadHandler_peakColor = new Color(0, 0, 0, 1);
        [BoxGroup("HCGテンプレ/入力"), InfoBox("画面幅に対して、指をどれくらい動かすべきか"), Range(0, 1)]
        public float fingerRange = 0.4f;
        [BoxGroup("HCGテンプレ/入力"), LabelText("UE上のマウステスト用の感度倍率")] public float sensivityOnEditor = 4;
        [BoxGroup("HCGテンプレ/入力"), LabelText("右中クリックしたときTimescaleを変更する")] public bool activeTimescaleChanger = true;
        [BoxGroup("HCGテンプレ/入力"), LabelText("右クリックしたときのTimescale")] public float timescale_R = 2;
        [BoxGroup("HCGテンプレ/入力"), LabelText("中クリックしたときのTimescale")] public float timescale_C = 3;

        [BoxGroup("HCGテンプレ/キャラクター"), LabelText("歩行速度")] public float characterWalkSpeed;
        [BoxGroup("HCGテンプレ/キャラクター"), LabelText("振り向き感度")] public float characterAdjRotParameter;

        public PD_DropId dropId_error;
        public PD_DropIdList dropIdList;
        public int characterNumber = 3;
        public Mass pfMass;
    }
}
