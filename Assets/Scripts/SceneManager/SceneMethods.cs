using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーン管理に必要であろう処理の記述をまとめていく
public class SceneMethods : MonoBehaviour
{
    // 現在のインデックス数
    protected int currentSceneIndex;
    // 次のシーンのインデックス数
    protected int nextSceneIndex;
    // 次のシーンのインデックスを取得するメソッド
    public virtual void GetNextSceneIndex()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
    }
    // 次のシーンをLoadするメソッド
    public virtual void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
