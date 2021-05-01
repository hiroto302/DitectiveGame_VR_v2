using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 取っ手に手がふれている時、Player の Hand の位置を取得 対辺を取得するために利用
public class HandPoint : MonoBehaviour
{
    // 初期位置
    Vector3 initialPosition;
    // 移動後の位置
    // Vector3 movePosition;
    public Vector3 InitialPosition
    {
        get{ return initialPosition;}
    }
    // DoorHandleと同じ位置に配置
    void Reset()
    {
        GameObject doorHandle = GameObject.Find("DoorHandle");
        transform.localPosition = doorHandle.transform.localPosition;
    }
    void Awake()
    {
        // ローカル座標の初期位置
        initialPosition = transform.localPosition;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(gameObject.transform.position, 0.1f);
    }
}
