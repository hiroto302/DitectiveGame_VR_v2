using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FirstStageの表示する結果を制御するスクリプト
public class ResultShowController : MonoBehaviour
{
    // FirstStageの評価 0~3 static 変数
    public static int firstStageEvaluation = 3;
    // ScaleInside 内側にある聖杯の数
    [SerializeField]
    ScaleInside scaleInside = null;
    // LittleCatMessage 表示するmessageの制御
    [SerializeField]
    LittleCatMessage littleCatMessage = null;
    // StarController 表示する星の制御
    [SerializeField]
    StarController starController = null;
    // EvaluationMessage 表示する評価内容の制御
    [SerializeField]
    EvaluationMessage evaluationMessage = null;
    // 評価内容を記載するCanvasの制御
    [SerializeField]
    LittleCatUIController littleCatUIController = null;

    // 結果を表示するフラグ
    bool showResult = false;
    // 結果により表示させる内容を設定するフラグ
    bool setResult = true;
    // 猫のMessage
    bool catMessageShow = true;
    // 星の表示
    bool starShow = false;
    // 評価の文字
    bool  evaluationShow = false;
    // 経過時間
    float elapsedTime = 0;

    // プロパティ
    public bool EvaluationShow
    {
        get {return evaluationShow;}
    }

    void Reset()
    {
        // 各スクリプトの取得
        scaleInside = GameObject.Find("ScaleInside").GetComponent<ScaleInside>();
        littleCatMessage = GameObject.Find("LittleCat").GetComponentInChildren<LittleCatMessage>();
        starController = GameObject.Find("LittleCat").GetComponentInChildren<StarController>();
        evaluationMessage = GameObject.Find("LittleCat").GetComponentInChildren<EvaluationMessage>();
        littleCatUIController = GameObject.Find("LittleCat").GetComponentInChildren<LittleCatUIController>();
    }

    void Start()
    {
        // 初期化
        firstStageEvaluation = 3;
    }

    void Update()
    {
        if(showResult)
        {
            // 表示させる結果の設定
            if(setResult)
            {
                SetResult();
                // 設定完了
                setResult = false;
            }
            // UICanvasの表示
            littleCatUIController.ShowCanvas(true);
            // 猫のMessage表示
            if(catMessageShow)
            {
                littleCatMessage.StartMessage();
                catMessageShow = false;
            }
            // 星の表示
            if(starShow)
            {
                elapsedTime += Time.deltaTime;
                if(elapsedTime > 0.5f)
                {
                    starController.ShowStar();
                    starShow = false;
                    elapsedTime = 0;
                }
            }
            // 評価文字の表示
            if(evaluationShow)
            {
                elapsedTime += Time.deltaTime;
                if(elapsedTime > 1.0f)
                {
                    evaluationMessage.ShowEvaluation();
                    evaluationShow = false;
                    // 全ての評価表示完了
                    showResult = false;
                }
            }
        }
    }

    // 結果内容の設定
    // デリゲートを利用して記述 LittaleCatMesssageの略
    delegate string Lcm();
    public void SetResult()
    {
        Lcm[] messages = new Lcm[4];
        // littleCatのmessageを格納するデリゲート
        messages[0] = littleCatMessage.BadMessage;
        messages[1] = littleCatMessage.NormalMessage;
        messages[2] = littleCatMessage.GoodMessage;
        messages[3] = littleCatMessage.VeryGoodMessage;
        // 評価
        firstStageEvaluation -= scaleInside.insideChalice;
        // 評価に対応したlittleCatMessageを表示
        littleCatMessage.SetMessage(messages[firstStageEvaluation]());
        // 星の数
        starController.SetEvaluation(firstStageEvaluation);
    // 評価の内容
        evaluationMessage.SetMessage();
    }
    // 結果を表示するメソッド
    public void ShowResult()
    {
        showResult = true;
    }
    // 星の表示を行うメソッド
    public void ShowStar()
    {
        starShow = true;
    }
    // 評価文字を表示するメソッド
    public void ShowEvaluation()
    {
        evaluationShow = true;
    }
}
