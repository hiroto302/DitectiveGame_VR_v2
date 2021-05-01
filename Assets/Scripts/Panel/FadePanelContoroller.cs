using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// PanelのImageを制御する 抽象クラス
// FadeOut & FadeIn 機能実装
public abstract class FadePanelContoroller : MonoBehaviour
{
    // 制御するPanelのImage
    [SerializeField]
    protected Image image = null;
    // Imageの各色の値
    float red, green, blue, alfa;
    // fadeさせるalfaの値
    float fadeAlfa;
    // 透明度が減少するスピード
    public float fadeOutSpeed;
    // 透明度が増加するスピード
    public float fadeInSpeed;
    // 経過時間
    float elapsedTime = 0;
    // fadeにかける時間
    float fadeOutTime, fadeInTime;
    // fadeIn, Out のフラグ
    public bool fadeOut = false;
    public bool fadeIn = false;
    void Reset()
    {
        // Imageの取得
        image = GetComponent<Image>();
    }
    void Awake()
    {
        // 初期の色
        InitialImageColor();
        // 各色の値取得
        red = image.color.r;
        green = image.color.g;
        blue = image.color.b;
        alfa = image.color.a;
    }

    // fadeにかける時間メソッド記述
    public abstract void Start();

    void Update()
    {
        if(fadeOut)
        {
            if(elapsedTime == 0)
            {
                // fadeAlfaの初期化
                fadeAlfa = alfa;
                // fadeSpeedの取得
                SetFadeOutSpeed(fadeOutTime, fadeAlfa);
            }
            elapsedTime += Time.deltaTime;
            // fadeOut実行
            FadeOut(elapsedTime);
        }

        if(fadeIn)
        {
            if(elapsedTime == 0)
            {
                fadeAlfa = alfa;
                SetFadeInSpeed(fadeInTime, fadeAlfa);
            }
            elapsedTime += Time.deltaTime;
            FadeIn(elapsedTime);
        }
    }

    // 初期のImageColorを記述
    public abstract void InitialImageColor();

    // 他のスクリプトからfadeOutを開始させるメソッド
    public void FadeOutStart()
    {
        fadeOut = true;
    }
    // fadeOutにかける時間をセットするメソッド
    protected void SetFadeOutTime(float time)
    {
        fadeOutTime = time;
    }
    // fadeOutにかける時間により、speedを決定するメソッド (引数 時間, alfaの値)
    void SetFadeOutSpeed(float time, float fadeAlfa)
    {
        fadeOutSpeed = 1.0f / time - fadeAlfa / time;
    }
    // fadeOutの機能
    void FadeOut(float elapsedTime)
    {
        // imageの表示
        if(image.enabled == false)
        {
            image.enabled = true;
        }
        // 透明度の上昇
        alfa = fadeAlfa + fadeOutSpeed * elapsedTime;
        // fadeの終了
        if(alfa > 1.0f)
        {
            fadeOut = false;
            this.elapsedTime = 0;
        }
        // 新たな色を反映
        SetAlpha();
    }

    public void FadeInStart()
    {
        fadeIn = true;
    }
    protected void SetFadeInTime(float time)
    {
        fadeInTime = time;
    }
    void SetFadeInSpeed(float fadeInTime, float fadeAlfa)
    {
        fadeInSpeed = fadeAlfa / fadeInTime;
    }

    void FadeIn(float elapsedTime)
    {
        if(image.enabled == false)
        {
            image.enabled = true;
        }
        // 透明度の減少
        alfa = 1.0f - fadeInSpeed * elapsedTime;
        if(alfa < 0)
        {
            fadeIn = false;
            this.elapsedTime = 0;
        }
        SetAlpha();
    }

    // 色を反映させるメソッド
    void SetAlpha()
    {
        image.color = new Color(red , green, blue, alfa);
    }
    // 黒色
    protected Color BlackColor()
    {
        return new Color(0, 0, 0, 1);
    }
    // 黒の透明色
    protected Color TransparentBlackColor()
    {
        return new Color(0, 0, 0, 0);
    }
}