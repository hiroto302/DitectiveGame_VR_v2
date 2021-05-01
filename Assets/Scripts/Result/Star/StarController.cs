using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 結果により表示するStarを制御するクラス
public class StarController : MonoBehaviour
{
    // 生成する星
    [SerializeField]
    GameObject Star = null;
    // StarにセットするMaterial
    [SerializeField]
    Material starMaterial = null;
    [SerializeField]
    // 生成する星の位置を格納した親
    Transform instantiatePointParent = null;
    // 生成する各位置
    Transform[] instantiatePoints;
    // 星の生成・表示開始フラグ
    bool showStar = false;
    // 生成する星の数 3個
    int countStar = 3;
    // 現在生成している星の数
    int nowCount = 0;
    // 評価の数 0~3
    int evaluation = 0;
    // 経過時間
    float elapsedTime = 0;
    // 生成するスピード
    float instantiateSpeed = 1.0f;

    [SerializeField]
    ResultShowController resultShowController = null;
    [SerializeField]
    SE se = null;

    void Reset()
    {
        resultShowController = GameObject.Find("LittleCat").GetComponentInChildren<ResultShowController>();
        se = GetComponent<SE>();
    }
    void Awake()
    {
        // 生成位置の設定
        instantiatePoints = new Transform[instantiatePointParent.transform.childCount];
        for(int i = 0; i < instantiatePointParent.transform.childCount; i++)
        {
            instantiatePoints[i] = instantiatePointParent.transform.GetChild(i);
        }
    }

    void Update()
    {
        if(showStar)
        {
            // 生成の終了
            if(nowCount >= countStar)
            {
                showStar =false;
                ShowEnd();
                return;
            }
            elapsedTime += Time.deltaTime;
            // 一定時間経過したら星の生成
            if(instantiateSpeed < elapsedTime)
            {
                GameObject star =  Instantiate(Star, instantiatePoints[nowCount].position, Quaternion.identity) as GameObject;
                // 評価の数により生成する星のMaterial変更
                if(evaluation > 0)
                {
                    // SE 星の生成音
                    se.PlaySE(0);
                    star.GetComponent<Renderer>().sharedMaterial = starMaterial;
                    evaluation --;
                }

                nowCount ++;
                elapsedTime = 0;
            }
        }
    }


    // 他スクリプトから星の表示を開始するメソッド
    public void ShowStar()
    {
        showStar = true;
    }

    // 評価の数の設定
    public void SetEvaluation(int n)
    {
        evaluation = n;
    }

    // 表示終了後の処理
    void ShowEnd()
    {
        resultShowController.ShowEvaluation();
    }
}
