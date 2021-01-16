using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    public class FastInstManager : SingletonMonoBehaviour<FastInstManager>
    {
        [ShowInInspector, ReadOnly]
        private List<IFastInst> listIFI = new List<IFastInst>(1000);

        [ShowInInspector, ReadOnly]
        private Dictionary<ParticleSystem, ParticleSystem> dicPaS = new Dictionary<ParticleSystem, ParticleSystem>(1000);
        protected override void UnityAwake() { }

        public void Add(IFastInst e) => listIFI.Add(e);
        public void Remove(IFastInst e) => listIFI.Remove(e);

        public void Add(ParticleSystem e, ParticleSystem prefab) => dicPaS.Add(e, prefab);
        public void Remove(ParticleSystem e) => dicPaS.Remove(e);

        // public void OnBattleSceneEnd()
        // {
        //     foreach (Transform t in OD_FastPoolManager.Ins.ErrorFolder)
        //     {
        //         Assert.UnReachable($"FastInstに失敗した：　{t.name}");
        //     }
        //     while (!listIFI.Empty())
        //     {
        //         var ifi = listIFI.RemoveHead();
        //         ifi.FastDestroy();
        //     }
        //     while (!dicPaS.Empty())
        //     {
        //         var pair = dicPaS.RemoveHead();
        //         pair.Key.FastDestroyParticle(pair.Value);
        //     }
        //     base.OnDestroy();
        // }
        // public static Vector3 GetPositionOnCameraCanvas(Vector3 worldPos)
        // {
        //     var worldCamera = Camera.main;
        //     var canvasRect = BattleCardParameters.Ins.cardCanvasTransform;

        //     var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, worldPos);
        //     var pos = Vector2.zero;
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out pos);

        //     return pos;
        // }
    }
}
