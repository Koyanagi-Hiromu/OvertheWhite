using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PPD
{
    [TypeInfoBox("Singletonなので大変アクセスしやすいです。")]
    public class Hero : SingletonMonoBehaviour<Hero>
    {
        protected override void UnityAwake()
        { }
    }
}

