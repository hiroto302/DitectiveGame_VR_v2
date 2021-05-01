using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Catの会話全体の流れを制御するスクリプトの抽象クラス
// Talk + 番号 メソッドで message や option を 制御し、会話を作成
public abstract class CatTalkController : MonoBehaviour
{
    [SerializeField]
    protected CatMessageController message = null;
    [SerializeField]
    protected CatMessages catMessages = null;
    [SerializeField]
    protected Cat cat = null;
    [SerializeField]
    protected OptionPanelController optionPanelController = null;
    [SerializeField]
    protected PlayerController playercontroller = null;

    // 分割文字
    protected string[] splitKeyWord = new string[1];
    // Talk を開始する判定
    public bool[] talk;
    // message を表示する判定
    public bool[] currentMessage;
    // 各message の Page数を格納
    protected int[] pageCount;

    void Reset()
    {
        message = GetComponentInChildren<CatMessageController>();
        catMessages = GetComponentInChildren<CatMessages>();
        cat = GetComponent<Cat>();
        optionPanelController = GetComponentInChildren<OptionPanelController>();
        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Awake()
    {
        // messageで使用している分割文字を格納
        splitKeyWord[0] = message.splitString;
        // pageCountの初期化Pa
        SetVariables();

    }
    // 会話内容をセットして開始するメソッド
    protected void StartTalk(string catMessage)
    {
        message.SetMessagePanel(catMessage);
        message.MessageStart();
    }
    // 1つのメッセージあたりの、ページー数(分割した回数・セパレーターの数)を取得するメソッド
    protected int PageCount(string splitTarget)
    {
        string[] page = splitTarget.Split(splitKeyWord, StringSplitOptions.None);
        return page.Length;
    }

    // talk・currentMessage・pageCountの変数を初期化するメソッド
    public abstract void SetVariables();
    // Update関数に話の流れの処理を記述する
    public abstract void Update();

}
