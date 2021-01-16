using System;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    public abstract class CreationSource : SploveScriptableObject
    {
        [DisplayAsString] public int pKey;
        [DisplayAsString] public int mainId;
        [DisplayAsString] public int subId;

        [InlineEditor(InlineEditorModes.LargePreview)] public Sprite sprite;
        public string title { get; private set; }
        public string desc { get; private set; }
        public int rarity;
        public int priorityInCatalog;
        public PublishLevel publishLevel;

        public void I2(string key)
        {
            title = Util.I2($"{key}/{pKey:00000}/name");
            desc = Util.I2($"{key}/{pKey:00000}/desc");
        }

#if UNITY_EDITOR
        [PropertySpace]
        [ShowInInspector, PropertyOrder(-2), HideLabel] string Explain1 => "▼個別パラメータ▼";
        [ShowInInspector, PropertyOrder(256), HideLabel] string Explain2 => "▲個別パラメータ▲";
        [PropertySpace]
        [Button(ButtonSizes.Gigantic), DisableInPlayMode, PropertyOrder(257)]
        public void SetId()
        {
            var m = Regex.Match(name, "[0-9]+");
            this.pKey = int.Parse(m.Value);
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}