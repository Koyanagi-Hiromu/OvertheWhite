using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PPD
{
    public class TestComponent : MonoBehaviour
    {        // Update is called once per frame
        void Update()
        {
            Debug.Log(this.transform.eulerAngles.x);
        }
    }
}

