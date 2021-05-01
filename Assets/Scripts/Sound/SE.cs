using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 各オブジェクトのSEを制御するスクリプト
public class SE : MonoBehaviour
{
    // AudioSource
    [SerializeField]
    public AudioSource audioSource = null;

    // SE
    [SerializeField]
    public AudioClip[] sE = null;

    // AudioSourceのデフォルト設定
    void DefaultSet()
    {
        // 優先度
        audioSource.priority = 128;
        // 音量
        audioSource.volume = 0.2f;
        // ループ 無効
        audioSource.loop = false;
        // 音の3D化
        audioSource.spatialBlend = 1;
        // 音の再生タイミング 無効
        audioSource.playOnAwake = false;
    }
    void Reset()
    {
        audioSource = GetComponent<AudioSource>();
        DefaultSet();
    }

    // 指定した音をデフォルト音量で再生するメソッド
    public void PlaySE(int n)
    {
        audioSource.PlayOneShot(sE[n], audioSource.volume);
    }
    // 指定した音と音量で再生するメソッド
    public void PlaySE(int n, float volume)
    {
        audioSource.PlayOneShot(sE[n], volume);
    }
    // 指定した音・音量・位置で再生するメソッド
    public void PlaySE(int n, Vector3 position, float volume)
    {
        // PlayCliptAtPointは,Static関数
        AudioSource.PlayClipAtPoint( sE[n], position, volume);
    }
}
