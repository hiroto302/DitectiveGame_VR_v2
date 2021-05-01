using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 特定の範囲において猫との会話を制御するクラス
public class CatTalkScopeArea : TalkScopeArea
{
    // 同階層に会話文を格納
    [SerializeField]
    CatMessages catMessages = null;
    [SerializeField]
    CatTalkController_1Stage catTalkController = null;
    // Catのスクリプト
    Cat cat;
    // Playerのタグ名
    const string PlayerTag = "Player";

    // SE
    [SerializeField]
    SE se = null;
    void Start()
    {
        // 各変数の取得
        if(catMessages == null)
        {
            catMessages = GetComponent<CatMessages>();
        }
        if(catTalkController == null)
        {
            catTalkController = transform.root.gameObject.GetComponent<CatTalkController_1Stage>();
        }
        cat = transform.parent.gameObject.GetComponent<Cat>();
        se = GetComponent<SE>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(PlayerTag))
        {
            showIcon = true;
            // アイコンを表示
            talkIcon.SetActive(true);
            // アイコンを表示する音
            se.PlaySE(0);
        }
    }
    void OnTriggerStay(Collider other)
    {
        // 相手がPlayerかつNormal状態である時
        if(other.gameObject.CompareTag(PlayerTag) && playerController.currentState != PlayerController.State.Talk)
        {
            talkIcon.SetActive(true);
            // 会話開始
            if(Input.GetKeyDown(KeyCode.T) || OVRInput.GetDown(OVRInput.Button.One))
            {
                cat.SetState(Cat.State.Talk);
                // Playerを会話状態に変更
                playerController.SetState(PlayerController.State.Talk);
                // アイコンを非表示
                talkIcon.SetActive(false);
                showIcon = false;
                // 一言目を表示
                catTalkController.Talk1();
            }
        }
        // 会話状態の時
        else if(other.gameObject.CompareTag(PlayerTag) && playerController.currentState == PlayerController.State.Talk)
        {
            // アイコン非表示
            talkIcon.SetActive(false);
        }
    }
    void Update()
    {
        if(showIcon == true)
        {
            IconDirection();
        }
    }
}
