using UnityEngine;
namespace PPD
{
    public static class EX_PrefabInst
    {
        // public static void FastDestroy_Ex<T>(this T bh)
        // where T : Component, IFastInst
        // {
        //     if (bh == null) return;

        //     if (FastInstManager.Ins != null)
        //     {
        //         FastInstManager.Ins.Remove(bh);
        //     }

        //     var t = bh.transform;
        //     t.localPosition = Vector3.zero;
        //     t.localRotation = Quaternion.identity;
        //     _FastDestroy(bh.gameObject, bh.SourcePf);
        // }

        // public static void FastDestroy_ExByIFI<T>(this T bh)
        // where T : Component
        // {
        //     if (bh == null) return;

        //     var ifi = bh.GetComponent<IFastInst>();
        //     if (ifi == null) return;

        //     if (FastInstManager.Ins != null)
        //     {
        //         FastInstManager.Ins.Remove(ifi);
        //     }
        //     _FastDestroy(bh.gameObject, ifi.SourcePf);
        // }

        // public static void FastDestroyParticle(this ParticleSystem bh, ParticleSystem sourcePrefab)
        // {
        //     if (bh == null) return;

        //     if (FastInstManager.Ins != null)
        //     {
        //         FastInstManager.Ins.Remove(bh);
        //     }
        //     _FastDestroy(bh.gameObject, sourcePrefab);
        // }

        // private static void _FastDestroy(GameObject objectToDestroy, Component sourcePrefab)
        // {
        //     var pool = FastPoolManager.GetPool(sourcePrefab, true);
        //     if (!objectToDestroy.activeSelf)
        //     {
        //         if (pool.Contains(objectToDestroy))
        //         {
        //             return;
        //         }
        //     }
        //     pool.FastDestroy(objectToDestroy);
        // }

        // public static T FastInst<T>(this T prefab, Transform folder)
        // where T : Component, IFastInst
        //  => _FastInst(prefab, prefab.gameObject.FastInstantiate(folder));

        // public static T FastInst<T>(this T prefab, Vector3 worldPos)
        // where T : Component, IFastInst
        //  => _FastInst(prefab, prefab.gameObject.FastInstantiate(worldPos, Quaternion.identity, null));

        // public static T FastInst<T>(this T prefab, Component parent, Vector3 worldPos)
        // where T : Component, IFastInst
        //  => _FastInst(prefab, prefab.gameObject.FastInstantiate(worldPos, Quaternion.identity, parent.transform));

        // public static ParticleSystem FastInst(this ParticleSystem prefab, Transform folder)
        // => _FastInst(prefab, prefab.gameObject.FastInstantiate(folder));

        // public static ParticleSystem FastInst(this ParticleSystem prefab, Transform folder, Vector3 worldPos)
        // => _FastInst(prefab, prefab.gameObject.FastInstantiate(worldPos, Quaternion.identity, folder));

        // private static T _FastInst<T>(T prefab, GameObject inst)
        // where T : Component, IFastInst
        // {
        //     var ret = inst.GetComponent<T>();
        //     ret.SourcePf = prefab;
        //     if (FastInstManager.Ins != null)
        //     {
        //         FastInstManager.Ins.Add(ret);
        //     }
        //     return ret;
        // }

        // private static ParticleSystem _FastInst(ParticleSystem prefab, GameObject inst)
        // {
        //     var ret = inst.GetComponent<ParticleSystem>();
        //     if (FastInstManager.Ins != null)
        //     {
        //         FastInstManager.Ins.Add(ret, prefab);
        //     }
        //     return ret;
        // }

        public static T Inst<T>(this GameObject prefab, Component parent, bool worldPositionStays = false)
         where T : Component
        {
            return Inst<T>(prefab, parent.gameObject, worldPositionStays);
        }

        public static T Inst<T>(this T prefab, GameObject parent = null)
         where T : Component
        {
            return Inst<T>(prefab.gameObject, parent, false);
        }

        public static T Inst<T>(this T prefab, Component parent)
         where T : Component
        {
            return Inst<T>(prefab.gameObject, parent, false);
        }

        public static T Inst<T>(this T prefab, Vector3 position)
         where T : Component
        {
            return UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
        }

        public static T Inst<T>(this T prefab, Transform parent, Vector3 position)
         where T : Component
        {
            return UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity, parent);
        }

        public static T Inst<T>(this GameObject prefab, GameObject parent = null, bool worldPositionStays = false)
         where T : Component
        {
            return Instantiate(prefab, parent, worldPositionStays).GetComponent<T>();
        }

        public static T Inst<T>(this T prefab, Transform parent, bool worldPositionStays = false)
         where T : Component
        {
            return Instantiate(prefab.gameObject, parent, worldPositionStays).GetComponent<T>();
        }

        public static GameObject Instantiate(this GameObject prefab, Component parent, bool worldPositionStays = false)
        {
            return Instantiate(prefab, parent ? parent.transform : null, worldPositionStays);
        }

        public static GameObject Instantiate(this GameObject prefab, GameObject parent, bool worldPositionStays = false)
        {
            return Instantiate(prefab, parent ? parent.transform : null, worldPositionStays);
        }

        public static GameObject Instantiate(this GameObject prefab, Transform parent = null, bool worldPositionStays = false)
        {
            if (parent)
                return UnityEngine.Object.Instantiate(prefab, parent, worldPositionStays);
            else
                return UnityEngine.Object.Instantiate(prefab);
        }
    }
}
