using Sirenix.OdinInspector;
using UnityEngine;

namespace SR
{
    public class ActiveInactiveHandler : MonoBehaviour
    {
        [Title("このオブジェクトがアクティブ✓になったとき、")]
        [SerializeField, LabelText("アクティブ✓にするオブジェクト")]
        private GameObject[] activeArray;
        [SerializeField, LabelText("非アクティブにするオブジェクト")] private GameObject[] inactiveArray;

        [Title("このオブジェクトが非アクティブになったとき、")]
        [SerializeField, LabelText("アクティブ✓にするオブジェクト")]
        private GameObject[] activeArrayOnDisable;
        [SerializeField, LabelText("非アクティブにするオブジェクト")] private GameObject[] inactiveArrayOnDisable;

        private void OnEnable()
        {
            activeArray.ForEach(e => e.SetActive(true));
            inactiveArray.ForEach(e => e.SetActive(false));
        }

        private void OnDisable()
        {
            activeArrayOnDisable.ForEach(e => e.SetActive(true));
            inactiveArrayOnDisable.ForEach(e => e.SetActive(false));
        }

        [Button("アクティブを入れ替える")]
        public void SwawpActive()
        {
            var temp = activeArray;
            activeArray = activeArrayOnDisable;
            activeArrayOnDisable = temp;
        }

        [Button("非アクティブを入れ替える")]
        public void SwawpInactive()
        {
            var temp = inactiveArray;
            inactiveArray = inactiveArrayOnDisable;
            inactiveArrayOnDisable = temp;
        }
    }
}
