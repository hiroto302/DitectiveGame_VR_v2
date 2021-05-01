using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Messageクラスを継承
// Catにおける、のMessageの制御を行うクラス
public class CatMessageController : Message
{
    // Playerの変数
    [SerializeField]
    PlayerController player = null;
    // CatTalkControllerの変数
    [SerializeField]
    CatTalkController_1Stage catTalkController = null;

    // ある一つのmessageの後に、次のmessage(or Option)を表示するか判定
    public bool nextMessage1 = false;
    void Reset()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        catTalkController = transform.root.gameObject.GetComponent<CatTalkController_1Stage>();
    }
    public override void MessageStart()
    {
        isStartMessage = true;
        // messageを表示の間PlayerをTalk状態に変更
        player.SetState(PlayerController.State.Talk);
    }
    public override void MessageEnd()
    {
        if(nextMessage1)
        {
            player.SetState(PlayerController.State.Talk);
            // 次の文表示
            catTalkController.currentMessage[1] = true;
            nextMessage1 = false;
        }
        else
        {
            // Playerの状態をNormalに変更
            player.SetState(PlayerController.State.Normal);
        }
    }
    // 他スクリプトで次のMessageを表示させるためのメソッド
    public void NextMessage1()
    {
        nextMessage1 = true;
    }
}
