using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR
{
    public class DoorCenterPoint : MonoBehaviour
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }

        [SerializeField] Axis RotationAxis;        // 回転軸
        public Vector3 AxisDirection;              // 軸の方向

        void OnValidate()
        {
            if(RotationAxis == Axis.X)
            {
                AxisDirection = Vector3.right;
            }
            else if(RotationAxis == Axis.Y)
            {
                AxisDirection = Vector3.up;
            }
            else if(RotationAxis == Axis.Z)
            {
                AxisDirection = Vector3.forward;
            }
        }

        void OnDrawGizmos()
        {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere (transform.position, 0.1f);
        }
    }
}
