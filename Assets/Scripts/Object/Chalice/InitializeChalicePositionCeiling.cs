using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 天井に聖杯が到達してしまった時、初期位置に戻すためのスクリプト
public class InitializeChalicePositionCeiling : MonoBehaviour
{
    GameObject chalice;
    Chalice chaliceScript;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Chalice")
        {
            // 侵入してきた聖杯の情報取得
            chalice = other.gameObject;
            chaliceScript = chalice.GetComponent<Chalice>();
            // Chaliceが子要素のため、親要素の位置を初期位置に戻す
            chalice.transform.root.rotation = chaliceScript.ParentInitialRotation;
            chalice.transform.root.position = chaliceScript.ParentInitialPosition;
            // 元の位置に戻ったことを知らせるSE
            chalice.GetComponent<SE>().PlaySE(2);
        }
    }
}
