using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TitleScene の PlayerPanel を制御スクリプト
// PanelController の FadeOut機能を利用
public class TitlePlayerPanelController : FadePanelContoroller
{
    public override void InitialImageColor()
    {
        // 初期の色 透明
        image.color = TransparentBlackColor();
    }
    public override void Start()
    {
        // fadeOutにかける時間 7秒
        SetFadeOutTime(7.0f);
    }

}
