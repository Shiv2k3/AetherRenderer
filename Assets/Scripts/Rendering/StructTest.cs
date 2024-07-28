using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Rendering
{
    public class StructTest : MonoBehaviour
    {
        [Serializable]
        struct Test
        {
            public int index;
            public int length;

            public void ResetValues()
            {
                index = 0;
                length = 0;
            }

            public void Set(int i, int l)
            {
                index = i;
                length = l;
            }
        }

        public int index;
        public int length;
        Test t;

        [ContextMenu("Reset T")]
        private void ResetT()
        {
            t.ResetValues();
        }

        [ContextMenu("Set T")]
        private void SetT()
        {
            t.Set(index, length);
        }

        [ContextMenu("Debug T")]
        void DebugT()
        {
            Debug.Log(t.index);
            Debug.Log(t.length);
        }
    }
}
