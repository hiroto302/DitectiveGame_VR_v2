using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボタンをおす回数を取得し、その回数を減らす時、一括表示・次のページへ行く判定をすればバグることなくこの動作させることができる
// 今回は_1stageの方で実装する
public class CatTalkController_1 : CatTalkController
{
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
        // pageCount初期化
        pageCount = new int[1];
    }

    // 各talkをスタートするメソッド
    // 冒頭の会話開始
    public void FirstContactTalk()
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


    public override void Update()
    {
        // Talk1
        if(talk[0])
        {
            // 一つ目のmessage表示
            if(currentMessage[0])
            {
                // messag1 を表示
                StartTalk(catMessages.Message1());
                // ボタンを押す必要回数を取得
                pageCount[0] = PageCount(catMessages.Message1());
                currentMessage[0] = false;
            }
            // message 1 表示中のボタンが押される度に引く
            if(pageCount[0] > 0)
            {
                if(Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.One))
                {
                    pageCount[0] --;
                }
                // message1 が 表示中は、2つ目のmessageを非表示
                currentMessage[1] = false;
            }
            else if(pageCount[0] == 0)
            {
                // message1 が終了後、2つめを表示
                currentMessage[1] = true;
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
                pageCount[0] = PageCount(catMessages.Message2());
                currentMessage[0] = false;
            }
            if(pageCount[0] > 0)
            {
                if(Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.One))
                {
                    pageCount[0] --;
                }
            }
            else if(pageCount[0] == 0)
            {
                // 一連の会話が終了したらPlayerの状態を戻す
                playercontroller.SetState(PlayerController.State.Normal);
                talk[1] = false;
            }
        }
        // Talk3
        if(talk[2])
        {
            if(currentMessage[0])
            {
                StartTalk(catMessages.Message3());
                pageCount[0] = PageCount(catMessages.Message3());
                currentMessage[0] = false;
            }
            if(pageCount[0] > 0)
            {
                if(Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.One))
                {
                    pageCount[0] --;
                }
            }
            else if(pageCount[0] == 0)
            {
                playercontroller.SetState(PlayerController.State.Normal);
                talk[2] = false;
            }
        }

        // 冒頭の会話
        if(firstContact)
        {
            if(currentMessage[0])
            {
                StartTalk(catMessages.FirstContactMessage());
                pageCount[0] = PageCount(catMessages.FirstContactMessage());
                currentMessage[0] = false;
            }
            if(pageCount[0] > 0)
            {
                if(Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.One))
                {
                    pageCount[0] --;
                }
            }
            else if(pageCount[0] == 0)
            {
                playercontroller.SetState(PlayerController.State.Normal);
                firstContact = false;
            }
        }
    }
}
