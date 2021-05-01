using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 聖杯に追加する重りの抽象クラス
public abstract class ScaleWeight : MonoBehaviour
{
    // 重りの種類名
    protected string typeName;
    // 各種の重さ
    protected float weight;
    public string TypeName
    {
        get{ return typeName;}
    }
    public float Weight
    {
        get{ return weight;}
    }

    // 聖杯に触れたら、聖杯に各重りを追加するメソッド
    public abstract void OnTriggerEnter(Collider other);
}
