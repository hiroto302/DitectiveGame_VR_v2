using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ドアの回転軸 中心点
public class DoorCenterPoint : MonoBehaviour
{
    void OnDrawGizmos()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere (transform.position, 0.1f);
    }
}
