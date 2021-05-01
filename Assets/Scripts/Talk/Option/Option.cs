using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 抽象クラス Option
// Playerの選択により実行される処理などを記述
public abstract class Option : MonoBehaviour
{
    [SerializeField]
    Text optionText = null;
    // 選択肢の名称(説明)
    public string optionName;
    // 選択肢の名称(説明)を記述
    public abstract string OptionName();
    // 選択を識別するためMaterial
    [SerializeField]
    protected MeshRenderer meshRenderer = null;
    // 初期の色(非選択時の色)
    protected Color color1 = new Color(250.0f / 255.0f, 250.0f/ 255.0f, 250.0f/ 255.0f);
    // 選択時の色
    protected Color color2 = new Color(110.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
    // Optionを表示しているPanelを操作するスクリプト
    [SerializeField]
    protected OptionPanelController optionPanelController = null;
    // PointingDirectionのタグ
    const string PointingDirectionTag = "PointingDirection";
    // SE
    [SerializeField]
    SE se = null;

    // 選択された時、実行するメソッド
    public abstract void OptionExecution();
    void Reset()
    {
        // Textの取得
        optionText = transform.GetChild(0).gameObject.GetComponent<Text>();
        // 選択肢の名称書き換え
        optionText.text = OptionName();
        // Materialの取得
        meshRenderer = GetComponent<MeshRenderer>();
        SetEmissionColor(color1);
        // OptionPanelControllerの取得
        optionPanelController = transform.root.gameObject.GetComponentInChildren<OptionPanelController>();
        // SEの取得 optionをまとめている親のOptionPanelから取得
        se = transform.parent.gameObject.GetComponent<SE>();
    }

    void Start()
    {
        meshRenderer.material.EnableKeyword("_EMISSION");
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(PointingDirectionTag))
        {
            // 選択肢に触れた時、色を変更
            SetEmissionColor(color2);
            // 触れた時の音
            se.PlaySE(0);
        }
    }
    void OnTriggerStay(Collider other)
    {
        // PlayerのPointingDirectionに触れていて、かつAボタンを押された時実行するメソッド
        if(other.gameObject.CompareTag(PointingDirectionTag) && OVRInput.GetDown(OVRInput.Button.One))
        {
            OptionExecution();
            // 選択時の音
            se.PlaySE(0);
        }
        // Debug作業中の記述
        if(other.gameObject.CompareTag(PointingDirectionTag) && Input.GetKeyDown(KeyCode.O))
        {
            OptionExecution();
            se.PlaySE(0);
        }
    }
    void OnTriggerExit(Collider other)
    {
        // 選択外になった時、色を初期状態に戻す
        if(other.gameObject.CompareTag(PointingDirectionTag))
        {
            SetEmissionColor(color1);
        }
    }
    // Emissionの色を変更するメソッド
    protected void SetEmissionColor(Color color)
    {
        meshRenderer.material.SetColor("_EmissionColor", color);
    }
}
