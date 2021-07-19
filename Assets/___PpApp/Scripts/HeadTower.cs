using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    public class HeadTower : SingletonMonoBehaviour<HeadTower>
    {
        public float headHeight;
        public List<Head> tower;
        public Rigidbody currentRigidbody;
        public Head Bottom => tower[0];
        protected override void UnityAwake()
        {
            //TODO: Joint
            foreach (var h in tower)
            {
                h.Init();
                h.transform.parent = transform;
            }

            RefreshLooks();
        }

        internal void TakeHead(Head head)
        {
            head.transform.parent = transform;
            tower.Add(head);
            RefreshLooks();
        }

        [Button]
        public void RemoveBottom()
        {
            var head = Bottom;
            head.SetDead();
            tower.RemoveAt(0);
            RefreshLooks();
            this.transform.AddY(headHeight + 0.03f);
        }

        void RefreshLooks()
        {
            for (int i = 0; i < tower.Count; i++)
            {
                var head = tower[i];
                head.transform.localPosition = new Vector3(0, headHeight, 0) * i;
                head.character.shadow.gameObject.SetActive(false);
            }
            tower[0].character.shadow.gameObject.SetActive(true);
        }
    }
}

