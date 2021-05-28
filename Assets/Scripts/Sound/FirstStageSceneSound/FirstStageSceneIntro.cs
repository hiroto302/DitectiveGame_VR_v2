using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 現在のシーンにおいて、シーンに関するの管理を行うクラス
// イントロの操作このクラスで行う
public class FirstStageSceneIntro : MonoBehaviour
{
    // Introの場面であるか
    bool isIntro = false;
    // 経過時間
    float elapsedTime = 0;
    // SE
    [SerializeField]
    SE se = null;
    [SerializeField]
    PlayerController playerController = null;
    [SerializeField]
    CatTalkController_1Stage catTalkController = null;
    [SerializeField]
    FirstStagePlayerPanelController playerPanelController = null;
    // playerの位置
    Transform player;
    // playerの足の位置
    Transform playerFoot;
    // 猫の位置
    Transform cat;
    // SEの番号
    int seNum = 0;
    // 足音の回数
    int stepNum = 0;
    void Reset()
    {
        se = GetComponent<SE>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        catTalkController = GameObject.Find("Cat").GetComponent<CatTalkController_1Stage>();
        playerPanelController = GameObject.Find("Player").GetComponentInChildren<FirstStagePlayerPanelController>();
    }
    void Awake()
    {
        // 参照先の取得
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerFoot = GameObject.Find("PlayerFoot").GetComponent<Transform>();
        cat = GameObject.Find("Cat").GetComponent<Transform>();
    }
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        // 1_stageでは最初猫の説明から始まるので、状態をTalkに変更
        if(scene.name == "1_stage")
        {
            playerController.SetState(PlayerController.State.Talk);
            // intro = true;
            isIntro = true;
        }
    }

    void Update()
    {
        if(isIntro)
        {
            StartIntro();
        }
    }

    void StartIntro()
    {
        elapsedTime += Time.deltaTime;
        switch(seNum)
        {
            case 0:
                if(elapsedTime > 1.0f)
                {
                    // 扉開く音
                    se.PlaySE(seNum, player.position, 0.3f);
                    seNum ++;
                    elapsedTime = 0;
                }
                break;
            case 1:
                if(elapsedTime > 1.0f)
                {
                    // 扉が閉じる音
                    se.PlaySE(seNum, player.position, 0.3f);
                    seNum ++;
                    elapsedTime = 0;
                }
                break;
            case 2:
                if(elapsedTime > 1.0f)
                {
                    // 扉がロックされる音
                    se.PlaySE(seNum, player.position, 0.3f);
                    seNum ++;
                    elapsedTime = 0;
                }
                break;
            case 3:
                if(elapsedTime > 1.0f)
                {
                    if(stepNum < 4)
                    {
                        // 足音
                        se.PlaySE(seNum, playerFoot.position, 0.3f);
                        stepNum ++;
                    }
                    else
                    {
                        // fadeIn開始
                        playerPanelController.FadeInStart();
                        seNum ++;
                    }
                    elapsedTime = 0;
                }
                break;
            case 4:
                if(elapsedTime > 3.0f)
                {
                    // fadeIn終了したあと
                    // 猫トーク開始
                    se.PlaySE(seNum, cat.position, 0.3f);
                    catTalkController.StartFirstContactTalk();
                    seNum ++;
                }
                break;
            case 5:
                // intro終了
                isIntro = false;
                break;
        }
    }
}
