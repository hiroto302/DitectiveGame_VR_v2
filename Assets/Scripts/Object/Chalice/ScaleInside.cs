using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 測りの内側にある聖杯の数を取得するクラス
public class ScaleInside : MonoBehaviour
{
    // 内側の聖杯の数
    public int insideChalice = 0;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Chalice")
        {
            // 内側の聖杯の数増加
            insideChalice ++;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Chalice")
        {
            // 内側の聖杯の数減少
            insideChalice --;
        }
    }
}
