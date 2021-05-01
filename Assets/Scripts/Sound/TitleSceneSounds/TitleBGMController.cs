using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TitleScene の BGM を制御するクラス
// 音のFadeOut機能追加
public class TitleBGMController : BGM
{
    // 音量減少のフラグ
    public bool fadeOut = false;
    // 減少率
    float decreaseRate;
    // 音量の減少にかける時間
    public float fadeTime = 7.0f;
    // 初期の音量
    float initialVolume;
    // 経過時間
    float elapsedTime = 0;

    void Start()
    {
        // 初期の音量取得
        initialVolume = audioSource.volume;
        // 減少率の取得
        decreaseRate = initialVolume / fadeTime;
    }

    void Update()
    {
        // fadeOut開始
        if(fadeOut)
        {
            elapsedTime += Time.deltaTime;
            // 音量の減少
            audioSource.volume = initialVolume - decreaseRate * elapsedTime;
            // fadeOut終了
            if(audioSource.volume < 0.01f)
            {
                fadeOut = false;
            }
        }
    }

    // 他のスクリプトからfadeOutを開始させるメソッド
    public void FadeOutStart()
    {
        fadeOut = true;
    }
}
