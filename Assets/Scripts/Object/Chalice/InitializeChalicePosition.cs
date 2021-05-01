using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 一定の場所(床・天井)に、一定時間ある時、聖杯を初期位置に戻すメソッド
public class InitializeChalicePosition : MonoBehaviour
{
    // 一定時間経過した時、初期位置に戻す
    float time = 5.0f;
    // time の初期値
    float initialTime;
    // 侵入しているかどうか
    bool invasion = false;
    // 侵入している聖杯の数 (一つの聖杯に対して処理をしていくため)
    int num = 0;
    // 侵入してきたChaliceの変数
    GameObject chalice;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Chalice")
        {
            num ++;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Chalice" )
        {
            chalice = other.gameObject;
            invasion = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Chalice")
        {
            num --;
            // １個から０個になる時
            if(num < 1)
            {
                invasion = false;
            }
        }
    }
    void Start()
    {
        initialTime = time;
    }
    void Update()
    {
        if(invasion)
        {
            time -= Time.deltaTime;
            if(time < 0)
            {
                // Chaliceが子要素のため、親要素の位置を戻す
                chalice.transform.root.rotation = chalice.GetComponent<Chalice>().ParentInitialRotation;
                chalice.transform.root.position = chalice.GetComponent<Chalice>().ParentInitialPosition;
                // 戻ったことを知らせるse
                chalice.GetComponent<SE>().PlaySE(2);
                // 初期値のリセット
                time = initialTime;
                invasion = false;
            }
        }
    }
}
