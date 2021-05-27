using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScaleDoorの機能実装
// 計りの重さが3以上の時のみ閉めることが可能
// 取手に触れたら、一定数閉まっていく
// 完全に閉めたら扉を固定
public class ScaleDoorController : MonoBehaviour
{
    // 扉(蓋)の状態
    // 開いている状態、閉めることが可能な状態,固定されている状態
    public enum State
    {
        Open,
        Close,
        Fixed,
    }
    // 扉の現在の状態
    public State currentState;

    [SerializeField]
    ChaliceScale chaliceScale = null;
    // 回転させる扉
    [SerializeField]
    Transform door = null;
    // 回転の中心軸
    [SerializeField]
    Transform centerPoint = null;
    // 扉が閉じる時の角速度
    float angularVelocity = -45.0f;
    // 扉が閉めきれられる角度;
    float closedValue = 155.0f;
    // SE
    [SerializeField]
    SE se = null;
    // 子要素のBlinkLight
    BlinkLight blinkLightScript = null;
    // Handのタグ名
    const string HandTag = "Hand";

    // 扉が閉じられ固定させた時に発生する event
    public delegate void ChangeState();
    public static event ChangeState onFixedStateChange;

    void Reset()
    {
        chaliceScale = transform.root.gameObject.GetComponentInChildren<ChaliceScale>();
        door = transform.parent.gameObject.transform.parent;
        centerPoint = transform.parent.Find("CenterPoint");
        blinkLightScript = GetComponentInChildren<BlinkLight>();
        se = GetComponent<SE>();
    }

    void Start()
    {
        // ドアの状態の初期化
        SetState(State.Open);
        blinkLightScript = GetComponentInChildren<BlinkLight>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(HandTag) && currentState != State.Fixed)
        {
            SetState(State.Close);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(HandTag) && currentState != State.Fixed)
        {
            SetState(State.Open);
        }
    }

    void Update()
    {
        // ドアが固定状態かつ、計りの重さが3以上の時閉めることが可能
        if(currentState == State.Close && chaliceScale.totalWeight >= 3.0f )
        {
            // ドアを閉める
            door.RotateAround( centerPoint.position, door.forward.normalized, angularVelocity * Time.deltaTime);
            // closedValue 以上扉を閉めた時、扉を固定(閉め切った状態)
            closedValue += angularVelocity * Time.deltaTime;
            if(closedValue < 0)
            {
                SetState(State.Fixed);
            }
        }
    }

    // 状態を変更するメソッド
    public void SetState(State state)
    {
        currentState = state;
        if(state == State.Fixed)
        {
            // 固定される音
            se.PlaySE(0, 0.35f);
            // 聖杯を掴めない状態にする
            GameObject[] chalices = GameObject.FindGameObjectsWithTag("Chalice");
            foreach (var chalice in chalices)
            {
                chalice.GetComponent<Chalice>().NoGrabbable();
            }
            // ライトをoffにする
            blinkLightScript.LightSetActive(false);
            // Scaleのドアが閉まるevent発生
            onFixedStateChange();
        }
    }
}
