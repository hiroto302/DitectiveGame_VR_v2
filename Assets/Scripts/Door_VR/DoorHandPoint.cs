using UnityEngine;

namespace VR
{
    // 取っ手に手がふれている時、Player の Hand の位置を取得 対辺を取得するために利用
    // DoorHandleと同じ階層に配置
    public class DoorHandPoint : MonoBehaviour
    {
        Vector3 initialPosition;                // 初期位置
        public Vector3 InitialPosition
        {
            get{ return initialPosition;}
        }

        void Reset()
        {
            GameObject doorHandle = GameObject.Find("DoorHandle");
            transform.localPosition = doorHandle.transform.localPosition;
        }
        void Awake()
        {
            // ローカル座標の初期位置 = 取っ手の位置
            initialPosition = transform.localPosition;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(gameObject.transform.position, 0.1f);
        }
    }
}
