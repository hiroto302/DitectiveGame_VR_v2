using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LittleCatのUIをまとめるCanvasを制御するクラス
public class LittleCatUIController : MonoBehaviour
{
    // LittleCatのUI用のCanvas
    Canvas canvas;
    void Start()
    {
        // Canvasの取得
        canvas = GetComponent<Canvas>();
        // 初期は非表示
        ShowCanvas(false);
    }
    // canvasの表示・非表示を制御するメソッド
    public void ShowCanvas(bool show)
    {
        canvas.enabled = show;
    }



}
