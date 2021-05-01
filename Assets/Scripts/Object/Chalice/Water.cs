using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 水 聖杯に追加できる重り
public class Water : ScaleWeight
{
    // Chaliceのタグ名
    const string ChaliceTag  = "Chalice";
    // SE
    [SerializeField]
    SE se = null;

    // コンストラクタ
    public Water()
    {
        typeName = "Water";
        weight = 0.5f;
    }
    void Reset()
    {
        // SE の取得
        se = GetComponent<SE>();
    }
    // 聖杯に触れたら、聖杯に水の重りを追加
    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(ChaliceTag))
        {
            Chalice chaliceScript = other.gameObject.GetComponent<Chalice>();
            // 聖杯の状態が空の時、下記を実行
            if(chaliceScript.currentState == "empty")
            {
                chaliceScript.AddWeight(weight);
                chaliceScript.ChangeState(1);
                chaliceScript.InstantiateWater();
                chaliceScript.containedScale = typeName;
                // 聖杯に水が入る音
                se.PlaySE(0);
            }
        }
    }
    // 重りが追加された時、DropObject動作開始
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(ChaliceTag))
        {
            other.gameObject.GetComponentInChildren<DropObject>().ChangeIsKinematic();
        }
    }
}
