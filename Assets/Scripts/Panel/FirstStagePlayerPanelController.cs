using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStagePlayerPanelController : FadePanelContoroller
{
    public override void InitialImageColor()
    {
        // 初期の色黒
        image.color =  BlackColor();
    }
    public override void Start()
    {
        // 3秒かけてFadeIn
        SetFadeInTime(3.0f);
        // 5秒かけてFadeOut
        SetFadeOutTime(5.0f);
    }
}
