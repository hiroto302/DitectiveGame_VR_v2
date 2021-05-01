using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TitleSceneにおけるシーン管理を行うクラス
public class TitleSceneManager : SceneMethods
{
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
    }
    void Start()
    {
        // 次のシーンインデックスを取得
        GetNextSceneIndex();
    }

    void Update()
    {
        if((OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.A)) && !gameStart)
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
}
