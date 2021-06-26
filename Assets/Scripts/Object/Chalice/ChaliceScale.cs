using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 測り 聖杯の合計の重さを測定する
public class ChaliceScale : MonoBehaviour
{
    // 重さの合計値
    public float totalWeight = 0;
    // 重さの合計値により決定する、移動場所を格納している親オブジェクト
    [SerializeField]
    Transform scalePointsParent = null;
    // 移動する位置
    Transform[] scalePoints;
    // 移動させる基準点
    [SerializeField]
    Transform standardPoint = null;
    // 現在の位置
    Vector3 currentPosition;
    // 次移動する位置
    Vector3 nextPosition;
    // 現在位置から次の移動位置までの距離 (0.2 間隔の距離)
    float distance;
    // 移動までにかかる時間 (移動にかかるフレームの回数)
    float second = 0;
    // 移動スピード (0.2 を割り切ることができる値)
    float speed = 0.002f;
    // 符号 正(up) or 負(down)
    float sign;
    // Chaliceの計測・侵入フラグ
    bool intoChaliceScale = false;
    // totalWeightが3.0以上でScaleDoorを閉じることができる条件を満たしている時
    bool fullWeight = false;
    // Chaliceのタグ名
    const string ChaliceTag = "Chalice";

    // ドアの点滅
    BlinkLight blinkLightScript = null;

    // SE
    [SerializeField]
    SE se = null;
    bool playSE = true;

    void Start()
    {
        // 移動位置の設定
        scalePoints = new Transform[scalePointsParent.transform.childCount];
        for(int i = 0; i < scalePointsParent.transform.childCount; i++)
        {
            scalePoints[i] = scalePointsParent.transform.GetChild(i);
        }
        // 現在位置の初期化
        currentPosition = scalePoints[(int)totalWeight].position;
        standardPoint.position = scalePoints[Mathf.FloorToInt(totalWeight)].position;
        // 点滅スクリプトの取得
        blinkLightScript = GetComponentInChildren<BlinkLight>();
    }

    // 測り置かれている重りの取得
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(ChaliceTag))
        {
            intoChaliceScale = true;
            // 聖杯の位置更新
            other.gameObject.GetComponent<Chalice>().SetChalicePosition(Chalice.ChalicePosition.InScale);
            // ChaliceScaleの移動中、中身がこぼれないようにするための記述
            if(other.gameObject.GetComponent<Chalice>().currentState == "fill")
            {
                other.gameObject.GetComponent<Chalice>().StopDropObject();
            }
        }
    }
    // 測り置かれている重りの取得
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag(ChaliceTag))
        {
            // 聖杯を置いた時、重さが増加
            if((OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 0 && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 0 && intoChaliceScale == true))
                // || (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 1 && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 0 && intoChaliceScale == true))
            {
                totalWeight = GetTotalWeight();

                sign = -1.0f;
                // SE 音
                if(!fullWeight)
                {
                    se.PlaySE(0); // 移動音
                }
                // 総重量が3.0f以上の時
                if(totalWeight >= 3.0f)
                {
                    fullWeight = true;
                }
                // 完了音のフラグ
                if(fullWeight)
                {
                    playSE = true;
                }
                // chaliceを置いた時、侵入フラグの初期化
                intoChaliceScale = false;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(ChaliceTag))
        {
            // 置いてある重りを測りから取り除いた時、重さが減少
            if(!intoChaliceScale)
            {
                totalWeight -= other.gameObject.GetComponent<Chalice>().weight;
                if(totalWeight < 3.0f)
                {
                    fullWeight = false;
                }
                sign = 1.0f;
                // SE 音
                // totalWeightが減少し,3.0以下になった時音を鳴らす
                if(!fullWeight)
                {
                    se.PlaySE(0);  // 移動音
                }
                other.gameObject.GetComponent<Chalice>().SetChalicePosition(Chalice.ChalicePosition.OutScale);
            }
            // DropObjectの動作再開
            if(other.gameObject.GetComponent<Chalice>().currentState == "fill")
            {
                other.gameObject.GetComponent<Chalice>().MoveDropObject();
            }
            // 聖杯を取り出した時、侵入フラグの初期化
            intoChaliceScale = false;
        }
    }

    void Update()
    {
        // 次に移動する場所
        // 重さの変化に合わせ、移動場所を決定
        if(!fullWeight)
        {
            nextPosition = scalePoints[Mathf.FloorToInt(totalWeight)].position;
        }
        else if(fullWeight)
        {
            nextPosition = scalePoints[3].position;
        }

        if(currentPosition != nextPosition)
        {
            // 移動中,聖杯を掴めなくする
            ChangeGrabbableState(false);
            // 距離の取得 (0.2の倍数で取得するために下記の処理を行う)
            distance = Mathf.Round((nextPosition - currentPosition).magnitude * 10.0f) * 0.1f;
            // 移動にかかる時間の取得
            second = distance / speed ;
            currentPosition = nextPosition;
        }
        // 測りの移動
        if(second > 0)
        {
            standardPoint.Translate(new Vector3(0, sign * speed, 0));
            second -= 1.0f;
            if(second == 0)
            {
                // 聖杯を掴めるようにする
                ChangeGrabbableState(true);
            }
        }

        // SE 重さが3以上の時の位置に移動が完了したことを知らせる音 (小数点以下の僅かな差が生まれるので下記の処理を行う)
        if( Mathf.Abs(scalePoints[3].position.y - standardPoint.position.y) < 0.01f )
        {
            if(playSE)
            {
                se.PlaySE(1);
                playSE = false;
            }
            // ScaleDoorを閉めることが可能なことを示すために、ライトの点滅
            blinkLightScript.Blink(true);
        }
        else
        {
            // 閉じることができない時、点滅オフ
            if(blinkLightScript.blink)
            {
                blinkLightScript.Blink(false);
            }
        }
    }
    // Scaleの中にある聖杯を取得して、掴めない・掴める状態にするメソッド
    public void ChangeGrabbableState(bool grabbable)
    {
        // 全ての聖杯を取得
        GameObject[] chalices = GameObject.FindGameObjectsWithTag("Chalice");
        foreach (GameObject chalice in chalices)
        {
            Chalice chaliceScript = chalice.GetComponent<Chalice>();
            // 測りの中にある聖杯のみ掴めない・掴める状態にする
            if(chaliceScript.currentPosition == Chalice.ChalicePosition.InScale)
            {
                chalice.transform.parent.gameObject.GetComponent<BoxCollider>().enabled = grabbable;
            }
        }
    }
    // Scaleの中にある聖杯の合計の重さを取得
    public float GetTotalWeight()
    {
        // totaliWeightの初期化
        totalWeight = 0;
        // 全ての聖杯を取得
        GameObject[] chalices = GameObject.FindGameObjectsWithTag("Chalice");
        foreach (GameObject chalice in chalices)
        {
            Chalice chaliceScript = chalice.GetComponent<Chalice>();
            // 測りの中にある聖杯の重さを取得
            if(chaliceScript.currentPosition == Chalice.ChalicePosition.InScale)
            {
                totalWeight += chaliceScript.weight;
            }
        }
        return totalWeight;
    }
}
