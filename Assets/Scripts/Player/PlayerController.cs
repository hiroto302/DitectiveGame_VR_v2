using UnityEngine;
using UnityEngine.SceneManagement;

// Playerのスクリプト
// オキュラスクエスト用 transform.TranslateによるPlayerの移動方法
// Translate の 移動法でカクツク原因は, time.deltaTimeを利用すると起こるのが一つの原因であった
// 現在の解決方法
// 1. FPSが約40であるため、その乗数をかける
//    ＝＞ 対象機種がオキュラスクエストであってもデバイスによる性能差が出る場合が問題である
public class PlayerController : MonoBehaviour
{
    // Playerの状態
    public enum State
    {
        Normal,
        Talk
    }
    // Playerの現在の状態
    public State currentState;
    // Playerの移動に関する変数群
    private float angleSpeed = 30.0f;
    private float moveSpeed = 1.35f;
    private float speedMultiplier = 0.02f;
    // 顔が向いてる方向
    [SerializeField]
    Transform moveTarget = null;

    // オキュラスクエストの入力値
    Vector2 inputLeftStick;
    Vector2 inputRightStick;
    float x, y;
    Vector3 move = Vector3.zero;

    // Playerが指を指している方向 PointingDirection
    // 右手のPointingDirection
    [SerializeField]
    GameObject rightPointingDirection = null;

    // PlayerFootから取得
    [SerializeField]
    SE se = null;
    // 経過時間
    float elapsedTime = 0;
    void Reset()
    {
        if(!moveTarget)
        {
            moveTarget = GetComponentInChildren<OVRCameraRig>().transform.Find("TrackingSpace/CenterEyeAnchor");
        }
        rightPointingDirection = GameObject.FindWithTag("PointingDirection");
        se = transform.Find("PlayerFoot").GetComponent<SE>();
    }

    void Awake()
    {
        SetState(State.Normal);
    }
    void Update()
    {
        // Normalの状態時、移動可
        if(currentState == State.Normal)
        {
            // 左スティック入力 角度・旋回
            inputLeftStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            if(inputLeftStick.sqrMagnitude > 0)
            {
                MoveDirection(inputLeftStick);
            }

            // 右スティック入力 方向・移動
            inputRightStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            if(inputRightStick.sqrMagnitude > 0)
            {
                MovePosition(inputRightStick);
            }
        }
    }

    // 旋回するメソッド
    void MoveDirection(Vector2 inputLeftStick)
    {
        transform.Rotate(new Vector3(0, inputLeftStick.x, 0) * angleSpeed * speedMultiplier);
    }

    // 移動するメソッド
    void MovePosition(Vector2 inputRightStick)
    {
        // x, y -1.0 ~ 1.0 の値
        x = inputRightStick.x;
        y = inputRightStick.y;
        move = (x * moveTarget.right.normalized + y * moveTarget.forward.normalized ) * moveSpeed * speedMultiplier;
        transform.Translate(move, Space.World);
        // 足音 方向・移動
        if(Mathf.Abs(x) > 0.8f || Mathf.Abs(y) > 0.8f)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > 0.8f)
            {
                se.PlaySE(0, 0.9f);
                elapsedTime = 0;
            }
        }
        else if(Mathf.Abs(x) > 0.5f || Mathf.Abs(y) > 0.5f)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > 1.0f)
            {
                se.PlaySE(0, 0.8f);
                elapsedTime = 0;
            }
        }
        else if(Mathf.Abs(x) > 0.1f || Mathf.Abs(y) > 0.1f)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > 1.5f)
            {
                se.PlaySE(2, 0.7f);
                elapsedTime = 0;
            }
        }
    }

    // 現在の状態を変更するメソッド
    public void SetState(State state)
    {
        currentState = state;
        if(state == State.Normal)
        {
            // Talk状態の時、非表示
            ShowPointingDirection(false);
        }
        else if(state == State.Talk)
        {
            // Talk状態の時、表示
            ShowPointingDirection(true);
        }
    }

    // PointingDirectionの表示・非表示
    // 会話状態の時のみ、左右の手のPointingDirectionを有効にする
    // optionの実行時、右手のAボタンを使用するので右手のPointingDirectionを表示することに変更
    void ShowPointingDirection(bool show)
    {
        rightPointingDirection.SetActive(show);
    }
}
