using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 砂 聖杯に追加できる重り
public class Sand : ScaleWeight
{
    // Chaliceのタグ名
    const string ChaliceTag = "Chalice";
    // SE
    [SerializeField]
    SE se = null;
    // コンストラクタ 変数の初期化
    public Sand()
    {
        typeName = "Sand";
        weight = 1.0f;
    }

    void Reset()
    {
        // SE の取得
        se = GetComponent<SE>();
    }
    // 聖杯に触れたら、聖杯に砂の重りを追加
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
                chaliceScript.InstantiateSand();
                chaliceScript.containedScale = typeName;
                // 砂が聖杯に入る音
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
