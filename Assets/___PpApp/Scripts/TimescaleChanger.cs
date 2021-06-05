using UnityEngine;

namespace PPD
{
    public class TimescaleChanger : PPD_MonoBehaviour
    {
        private void Awake()
        {
            this.SetEnable(Data.Ins.activeTimescaleChanger);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Time.timeScale *= Data.Ins.timescale_R;
            }
            if (Input.GetMouseButtonDown(2))
            {
                Time.timeScale *= Data.Ins.timescale_C;
            }
            if (Input.GetMouseButtonUp(1))
            {
                Time.timeScale /= Data.Ins.timescale_R;
            }
            if (Input.GetMouseButtonUp(2))
            {
                Time.timeScale /= Data.Ins.timescale_C;
            }
        }
    }
}

