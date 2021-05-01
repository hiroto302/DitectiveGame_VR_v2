using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TitleSceneにおけるシーン管理を行うクラス
public class ReTitleSceneManager : SceneMethods
{
    // ClearMessageTimeが表示されている時間
    float clearMessageTime = 7.0f;
    // ClearMessage表示
    bool showClearMessage = true;
    // ReTitleCanvas
    [SerializeField]
    GameObject reTitleSceneCanvas = null;
    // ゲーム開始フラグ
    bool gameStart = false;
    // 経過時間
    float elapsedTime = 0;

    // BGMのスクリプト
    [SerializeField]
    TitleBGMController bgmController = null;
    // PlayerPanelのスクリプト
    [SerializeField]
    TitlePlayerPanelController playerPanelController = null;
    // SEのスクリプト
    [SerializeField]
    SE se = null;

    void Reset()
    {
        // 他のスクリプト取得
        bgmController = GameObject.Find("BGM").GetComponent<TitleBGMController>();
        playerPanelController = GameObject.Find("Player").GetComponentInChildren<TitlePlayerPanelController>();
        se = GetComponent<SE>();
        reTitleSceneCanvas = GameObject.Find("ReTittleSceneCanvas");
    }
    void Start()
    {
        // 次に遷移するシーンインデックスを取得
        GetNextSceneIndex();
        // ReTitleSceneCanvasを非表示
        reTitleSceneCanvas.SetActive(false);
    }

    void Update()
    {

        if(showClearMessage)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > clearMessageTime)
            {
                elapsedTime = 0;
                showClearMessage = false;
                reTitleSceneCanvas.SetActive(true);
            }
        }

        if((OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A)) && !gameStart && !showClearMessage)
        {
            // クリック音再生
            se.PlaySE(0);
            // ゲーム開始
            gameStart = true;
        }

        if(gameStart)
        {
            elapsedTime += Time.deltaTime;
            // 8秒後次のシーンに移行
            bgmController.FadeOutStart();
            playerPanelController.FadeOutStart();
            if(elapsedTime > 8.0f)
            {
                LoadNextScene();
            }
        }
    }

    // Index 1 を取得
    public override void GetNextSceneIndex()
    {
        nextSceneIndex = 1;
    }
}
