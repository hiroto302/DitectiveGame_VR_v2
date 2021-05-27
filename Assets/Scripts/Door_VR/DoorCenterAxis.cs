using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR
{
    // Door の中心軸
    [System.Serializable]
    public class DoorCenterAxis
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }

        public Axis CenterAxis;
    }

}
