using Deform;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    [ExecuteInEditMode]
    public class ED_DeformTest : ED_AutoMoveTest
    {
        protected override float Interval => 2.0f;
        float lapse;
        IFactor _deformable;
        IFactor Deformable
        {
            get
            {
                if (_deformable == null)
                {
                    _deformable = GetComponent<IFactor>();
                }
                return _deformable;
            }
        }

        protected override void UnityUpdate(float ratio)
        {
            Deformable.Factor = (ratio - 0.5f) * 2;
        }
    }
}