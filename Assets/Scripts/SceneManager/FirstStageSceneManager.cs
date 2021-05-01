using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// FirstStageにおけるシーン管理を行うクラス
public class FirstStageSceneManager : SceneMethods
{
    // 次のシーン移行へのフラグ
    bool nextScene = false;
    // 経過時間
    float elapsedTime = 0;
    // 他のスクリプト
    [SerializeField]
    ResultShowController resultShowControllerScript = null;
    [SerializeField]
    FirstStagePlayerPanelController playerPanelController = null;

    // チャレンジ回数
    public static int ChallengeCount = 1;

    void Reset()
    {
        resultShowControllerScript = GameObject.Find("LittleCat").GetComponentInChildren<ResultShowController>();
        playerPanelController = GameObject.Find("Player").GetComponentInChildren<FirstStagePlayerPanelController>();
    }
    void Start()
    {
        // 次のシーンインデックスを取得
        GetNextSceneIndex();
    }

    void Update()
    {
        // ゲーム結果の評価文字を表示したら次のシーンに移行開始
        if(resultShowControllerScript.EvaluationShow)
        {
            nextScene = true;
        }
        // シーン移行処理
        if(nextScene)
        {
            elapsedTime += Time.deltaTime;
            if(8.0f < elapsedTime)
            {
                // 獲得評価が３の時
                if(ResultShowController.firstStageEvaluation == 3)
                {
                    // 次のシーンへ遷移
                    LoadNextScene();
                }
                else
                {
                    // チャレンジ回数の増加
                    ChallengeCount++;
                    // 現在のシーンをロード
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else if(3.0f < elapsedTime)
            {
                // 5秒かけてFadeOut開始
                playerPanelController.FadeOutStart();
            }
        }
    }
}
