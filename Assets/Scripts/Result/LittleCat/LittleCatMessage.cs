using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// LittleCatが話す内容
public class LittleCatMessage : MonoBehaviour
{
    // measageを表示させるText
    Text messageText;
    // 表示するmessage
    string littleCatMessage = "猫のmessage";
    // テキストスピード
    float textSpeed = 0.05f;
    // 経過時間
    float elapsedTime = 0;
    // 現在表示している文字の番号
    int nowTextNum = 0;

    // 会話開始のフラグ
    bool startMessage = false;
    // 会話終了のフラグ
    bool endMessage = false;

    // 結果内容を制御するスクリプト
    [SerializeField]
    ResultShowController resultShowController = null;
    // SE
    [SerializeField]
    SE se = null;

    void Reset()
    {
        // 他スクリプトの取得
        resultShowController = GameObject.Find("LittleCat").GetComponentInChildren<ResultShowController>();
        se = GetComponent<SE>();
    }

    void Awake()
    {
        // Textの取得
        messageText = GetComponent<Text>();
        // textの初期化
        messageText.text = "";
    }

    void Update()
    {
        // Messageスタート
        if(startMessage)
        {
            // Message終了
            if(endMessage)
            {
                startMessage = false;
                EndMessage();
                return;
            }
            // 時間の計測
            elapsedTime += Time.deltaTime;
            // 文字の追加
            if(elapsedTime > textSpeed)
            {
                messageText.text += littleCatMessage[nowTextNum];
                // 値の更新
                nowTextNum ++;
                elapsedTime = 0;
                // messageを全て表示したか
                if(nowTextNum >= littleCatMessage.Length)
                {
                    endMessage = true;
                }
            }
        }
    }

    // 表示させるメッセージの設定
    public void SetMessage(string message)
    {
        littleCatMessage = message;
    }
    // メッセージをスタートさせるメソッド
    public void StartMessage()
    {
        startMessage = true;
        // SE 猫の声
        se.PlaySE(0);
    }

    // メッセージ終了後に行う処理
    void EndMessage()
    {
        // 星の表示
        resultShowController.ShowStar();
    }


    // 表示するmessage
    // 星３
    public string VeryGoodMessage()
    {
        string message = "すごいにゃ！見込みがあるにゃ！";
        return message;
    }
    // 星２
    public string GoodMessage()
    {
        string message = "  ふつーにゃ... 。";
        return message;
    }
    // 星1
    public string NormalMessage()
    {
        string message = "  だめだめにゃ！";
        return message;
    }
    // 星0
    public string BadMessage()
    {
        string message = "  出直してくるにゃ！";
        return message;
    }

}
