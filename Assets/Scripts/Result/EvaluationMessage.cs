using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// FirstStageの結果により表示される評価の文字を制御するスクリプト
public class EvaluationMessage : MonoBehaviour
{
    // 評価内容を表示するText
    Text evaluationText;

    // 獲得した聖杯の数
    int getChalice = 0;

    // プロパティ
    public int GetChalice
    {
        set {getChalice = value;}
    }
    // SE
    [SerializeField]
    SE se = null;

    void Reset()
    {
        // SEの取得
        se = GetComponent<SE>();
    }
    void Awake()
    {
        // Textの取得
        evaluationText = GetComponent<Text>();
        // textの初期化
        evaluationText.text = "";
    }

    void Start()
    {
        // 非表示
        gameObject.SetActive(false);
    }

    // 他スクリプトから評価内容を表示させるメソッド
    public void ShowEvaluation()
    {
        gameObject.SetActive(true);
        // SE 表示音
        se.PlaySE(0);
    }
    // 内容の設定
    public void SetMessage()
    {
        this.getChalice = ResultShowController.firstStageEvaluation;
        evaluationText.text = Message(this.getChalice);
    }
    // 表示する内容
    public string Message(int evaluation)
    {
        string message = "評価 : 獲得した聖杯" + evaluation + "つ";
        return message;
    }
}
