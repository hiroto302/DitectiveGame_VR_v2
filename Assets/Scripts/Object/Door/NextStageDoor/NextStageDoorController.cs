using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageDoorController : MonoBehaviour
{
    // 回転の中心軸
    [SerializeField]
    Transform centerPoint = null;
    // 扉の回転角速度
    float anglerVelocity = 15.0f;
    // 扉を回転させる角度
    float openAngle = 30.0f;

    // scaleDoorの状態を取得
    [SerializeField]
    ScaleDoorController scaleDoorController = null;
    // DoorPats
    [SerializeField]
    GameObject doorParts = null;
    // 経過時間
    float elapsedTime = 0;
    // SE
    [SerializeField]
    SE se = null;
    // 音の発生
    bool playSE = true;

    void Reset()
    {
        // 各変数の取得
        centerPoint = transform.GetChild(0).Find("CenterPoint");
        scaleDoorController = GameObject.Find("ScaleDoor").GetComponentInChildren<ScaleDoorController>();
        doorParts = transform.GetChild(0).gameObject;
        se = GetComponent<SE>();
    }

    void Start()
    {
        // ドアをできないよにする
        doorParts.SetActive(false);
    }
    void Update()
    {
        // 測りのドアが閉めきら時、扉をopenAngleの値だけ開く
        if(scaleDoorController.currentState == ScaleDoorController.State.Fixed && openAngle > 0)
        {
            elapsedTime += Time.deltaTime;
            // 1秒後扉をOpen
            if(elapsedTime > 1.0f)
            {
                if(playSE)
                {
                    // 扉が開く音
                    se.PlaySE(0, 0.9f);
                    playSE = false;
                }
                gameObject.transform.RotateAround(centerPoint.position, Vector3.up, anglerVelocity * Time.deltaTime);
                openAngle -= anglerVelocity * Time.deltaTime;
                // ドアの開閉を可能にする
                if(doorParts.activeSelf == false)
                {
                    doorParts.SetActive(true);
                }
            }
        }
    }
}
