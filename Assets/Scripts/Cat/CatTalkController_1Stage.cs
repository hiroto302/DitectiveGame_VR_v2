using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTalkController_1Stage : CatTalkController
{
    [SerializeField]
    CatMessageController catMessageController = null;
    [SerializeField]
    SE se = null;
    // 最初の対面時の会話フラグ
    public bool firstContact = false;

    public override void SetVariables()
    {
        // talk初期化 falseを代入
        talk = new bool[3];
        for(int i = 0; i < talk.Length; i++)
        {
            talk[i] = false;
        }
        // currentMessage初期化 falseを代入
        currentMessage =  new bool[2];
        for(int i = 0; i < currentMessage.Length; i++)
        {
            currentMessage[i] = false;
        }
    }

    // 各talkをスタートするメソッド
    // 冒頭の会話開始
    public void StartFirstContactTalk()
    {
        firstContact = true;
        currentMessage[0] = true;
    }
    // ２度目以降の冒頭の会話
    public void SecondContactTalk()
    {
        firstContact = true;
        currentMessage[0] = true;
    }
    // 話しかけた時の会話開始
    public void Talk1()
    {
        // 最初に表示するもののみをtrue
        talk[0] = true;
        currentMessage[0] = true;
    }
    // 説明について会話開始
    public void Talk2()
    {
        talk[1] = true;
        currentMessage[0] = true;
    }
    // 特になしについて会話開始
    public void Talk3()
    {
        talk[2] = true;
        currentMessage[0] = true;
    }

    void Start()
    {
        if(catMessageController == null)
        {
            catMessageController = GetComponentInChildren<CatMessageController>();
        }
        if(se == null)
        {
            se = GetComponent<SE>();
        }
    }

    public override void Update()
    {
        // 冒頭の会話
        if(firstContact)
        {
            if(currentMessage[0])
            {
                // １度目の挑戦の時
                if(FirstStageSceneManager.ChallengeCount == 1)
                {
                    StartTalk(catMessages.FirstContactMessage());
                }
                // ２度目以降の挑戦の時
                else
                {
                    StartTalk(catMessages.SecondContactMessage());
                }
                currentMessage[0] = false;
                firstContact = false;
            }
        }
        // Talk1
        if(talk[0])
        {
            // 一つ目のmessage表示
            if(currentMessage[0])
            {
                // 猫の鳴き声
                if(Random.Range(0, 2) == 0)
                {
                    se.PlaySE(0);
                }
                else
                {
                    se.PlaySE(1);
                }
                // messag1 を表示
                StartTalk(catMessages.Message1());
                // 1つ目のmessageが終了後2つ目のmessageを開始, CatMessageControllerで処理
                catMessageController.NextMessage1();
                currentMessage[0] = false;
            }
            // 2つ目にoptionを表示
            if(currentMessage[1])
            {
                optionPanelController.ShowPanel(true);
                currentMessage[1] = false;
                // 会話終フラグをfalseに初期化すること
                talk[0] = false;
            }
        }

        // Talk2
        if(talk[1])
        {
            if(currentMessage[0])
            {
                StartTalk(catMessages.Message2());
                currentMessage[0] = false;
                talk[1] = false;
            }
        }
        // Talk3
        if(talk[2])
        {
            if(currentMessage[0])
            {
                StartTalk(catMessages.Message3());
                currentMessage[0] = false;
                talk[2] = false;
            }
        }

    }
}
