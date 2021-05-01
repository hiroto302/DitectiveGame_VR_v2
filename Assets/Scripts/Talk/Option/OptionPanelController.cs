using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// OptionPanelの制御を行うクラス
// UIをまとめている親オブジェクトにアタッチ
public class OptionPanelController : MonoBehaviour
{
    [SerializeField]
    GameObject optionPanel = null;
    [SerializeField]
    PlayerController player = null;
    // マウスクリックを促すアイコン
    [SerializeField]
    Image clickIcon = null;

    // クリックアイコンの点滅秒数
    float clickFlashTime = 0.2f;
    // 経過時間
    float elapsedTime = 0;
    void Reset()
    {
        optionPanel = GameObject.Find("OptionPanel");
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        clickIcon = transform.Find("OptionPanel/Image").GetComponent<Image>();
    }
    void Awake()
    {
        // 初期は非表示
        ShowPanel(false);
    }
    void Update()
    {
        // クリックアイコンの点滅
        if(optionPanel.activeSelf)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= clickFlashTime)
            {
                clickIcon.enabled = !clickIcon.enabled;
                elapsedTime = 0;
            }
        }
    }
    // OptionPanelの表示・非表示を制御するメソッド
    public void ShowPanel(bool show)
    {
        optionPanel.SetActive(show);
        // 表示している時.Player Talk状態する記述を書くとPanelとPalyerの距離によっては選択しづらくなる恐れがある
        // if(show)
        // {
        //     player.SetState(PlayerController.State.Talk);
        // }
        // if(!show)
        // {
        //     player.SetState(PlayerController.State.Normal);
        // }
    }

}
