using System;
using DG.Tweening;
using UnityEngine;

namespace PPD
{
    [RequireComponent(typeof(Character))]
    public class Head : PPD_MonoBehaviour
    {
        bool isDead;
        public Character character { get; private set; }
        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            if (character == null)
            {
                this.character = GetComponent<Character>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isDead) return;

            var tower = other.GetComponent<HeadTower>();
            if (tower != null)
            {
                tower.TakeHead(this);
            }
        }

        internal void SetDead()
        {
            isDead = true;
            transform.parent = null;
            character.spriteRenderer.spriteRenderer.color = Color.gray;
            character.billBoard.SetEnable(false);

            character.billBoard.transform.AddY(0.01f);
            DOTween.Sequence()
            .Append(
                 character.billBoard.transform
                .DORotate(new Vector3(90, 0, 0), 1)
                .SetEase(Ease.OutBounce)
            )
            .Append(
                character.billBoard.transform.DOScale(1.1f, 0.1f)
            // .SetEase(Ease.OutBack)
            )
            .Append(
                character.transform.DOScale(0, 0.2f)
                .SetEase(Ease.InCubic)
            )
            // .AppendInterval(0.3f);
            .OnKill(() => character.SetActive(false));
        }
    }
}
