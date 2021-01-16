using System;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    [Serializable]
    public class CreationSourceParameter
    {
    }

    public enum PublishLevel : int
    {
        通常品,
        イベント品,
        近日公開,
        非公開,
        ボツ,
    }

    public static class StPublichLevel
    {
        public static bool CanTakeInRumtime(this PublishLevel level)
         => level == PublishLevel.通常品 || level == PublishLevel.イベント品;

        public static bool IsVisibleOnDictionary(this PublishLevel level)
         => !level.IsHidden();

        public static bool IsHidden(this PublishLevel level)
         => level == PublishLevel.非公開 || level == PublishLevel.ボツ;
    }
}