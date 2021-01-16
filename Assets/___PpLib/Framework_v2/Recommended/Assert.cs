using System;
using UnityEngine;

namespace PPD
{
    public static class Assert
    {
        public static void IsFalse(bool expectedFalse, string text = "")
        {
            if (expectedFalse == true)
            {
                UnReachable(text);
            }
        }

        public static void IsTrue(bool expected, string text = "")
        {
            if (expected == false)
            {
                UnReachable(text);
            }
        }

        public static void Exist(System.Object obj, string text = "")
        {
            if (obj == null)
            {
                if (text.Length == 0)
                {
                    UnReachable();
                }
                else
                {
                    UnReachable(text);
                }
            }
        }

        public static void UnReachable(string text = "")
        {
            if (text == "")
            {
                text = "Assertion Error";
            }
            // SLog.Event.Error(text);
            Debug.LogException(new Exception(text));
        }
    }
}
