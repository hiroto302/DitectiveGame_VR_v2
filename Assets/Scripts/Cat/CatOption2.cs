using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatOption2 : Option
{
    [SerializeField]
    CatTalkController_1Stage catTalkController = null;
    void Awake()
    {
        if(catTalkController == null)
        {
            catTalkController = transform.root.gameObject.GetComponent<CatTalkController_1Stage>();
        }
    }
    public override string OptionName()
    {
        optionName = "特になし";
        return optionName;
    }
    public override void OptionExecution()
    {
        // optionの色リセッそ
        SetEmissionColor(color1);
        // 選択後 panelを非表示
        optionPanelController.ShowPanel(false);
        // Talk3を実行
        catTalkController.Talk3();
    }
}
