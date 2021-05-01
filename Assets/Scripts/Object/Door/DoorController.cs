using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ドアの開閉機能
// 必要なければ全て削除
public class DoorController : MonoBehaviour
{
    Rigidbody rb;

    // 子オブジェクトとして空のオブジェクトを、中心軸, 取っ手の部分としたい位置に配置。中心軸とHandleのx以外のpositionを同一にすること
    // ドアが開閉するときの中心軸
    [SerializeField]
    Transform centerPoint = null;
    // ドアの取っ手 DoorHandle
    [SerializeField]
    GameObject handle = null;
    Transform handleTransform;
    DoorHandle handleScript;
    [SerializeField]
    HandPoint handPointScript;

    // Gizmosの表示判定
    [SerializeField]
    bool testPlay = true;

    // 斜辺 (handleと中心軸の距離（半径） 一定)
    float radius;
    // 元の角度
    float formerAngle;
    // 後の角度
    float posteriorAngle;
    public float Radius
    {
        get{return radius;}
    }

    // handleの初期位置・移動後の位置
    Vector3 initialHandlePosition;
    Vector3 moveHandlePosition;


    void Reset()
    {
        centerPoint = transform.Find("DoorParts/CenterPoint");
        handle = transform.Find("DoorParts/DoorHandle").gameObject;
        handPointScript = transform.Find("DoorParts/HandPoint").gameObject.GetComponent<HandPoint>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // DoorHandleの変数 初期化
        handleTransform = handle.GetComponent<Transform>();
        handleScript = handle.GetComponent<DoorHandle>();
        // HandPointの変数 初期化
        // 半径の取得
        Vector3 dir = centerPoint.localPosition - handleTransform.localPosition;
        radius = dir.magnitude;
    }
    void Start()
    {

        // Handleの初期位置取得
        initialHandlePosition = handleTransform.localPosition;
    }

    void Update()
    {
        // 移動している時のhandleの位置取得
        // moveHandlePosition = handle.position;
        // 対辺 の値取得
        // float dz = Mathf.Abs(initialHandlePosition.z - moveHandlePosition.z);
        // float dz = initialHandlePosition.z - moveHandlePosition.z;
        // 隣辺 の値取得
        // float dx = Mathf.Abs(moveHandlePosition.x - centerPoint.position.x);
        // float dx = moveHandlePosition.x - centerPoint.position.x;
        // ２点のオイラー角取得
        // float rad = Mathf.Atan2(dz, dx);
        // ラジアンをオイラー角に変換
        // float deg = rad * Mathf.Rad2Deg;

        // Debug.Log(dz + ": dz");
        // Debug.Log(dx + ": dx");
        // Debug.Log(deg + "deg");
        // Debug.Log(transform.rotation);


        Debug.DrawLine (transform.position , centerPoint.position);
        if(Input.GetKey(KeyCode.P))
        {
            // ドアの回転処理
            transform.RotateAround(centerPoint.position, Vector3.up, 45*Time.deltaTime);
        }
        // 移動後の角度差
        // posteriorAngle = handleScript.Deg;
        // Doorハンドルから求めた角度差を利用し、ドアの回転処理を記述
        // 角度差回転
        // transform.RotateAround(centerPoint.position, Vector3.up, posteriorAngle - formerAngle);
        // 移動前の角度
        // formerAngle = handleScript.Deg;
        // 移動後 HandPointの位置をリセット
        // handPointScript.PositionReset();
    }

    // LateUpdateを使わずにやってみよ
    void LateUpdate()
    {
    }

    void OnDrawGizmos()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere (centerPoint.position, 0.1f);
            // Gizmos.DrawSphere (handleTransform.position, 0.1f);
    }
}
