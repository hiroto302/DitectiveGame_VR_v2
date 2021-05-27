using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR
{
    // Doorの中心軸の表示
    public class DisplayCenterAxisPositon : MonoBehaviour
    {
        void OnDrawGizmos()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere (transform.position, 0.1f);
    }
    }
}
