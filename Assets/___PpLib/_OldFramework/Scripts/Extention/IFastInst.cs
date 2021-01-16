using UnityEngine;

namespace SR
{
    public interface IFastInst
    {
        Component SourcePf { get; set; }
        void FastDestroy();
    }
}