using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 聖杯クラス

public class Chalice : MonoBehaviour
{
    // 聖杯の初期の重さ
    public float weight = 1.0f;
    // 聖杯の状態 空・満たされている状態の2つ
    string[] state = {"empty", "fill"};
    // 現在の状態
    public string currentState;
    // 現在加えられている重り
    public string containedScale = null;
    // 加えることができる重りである 砂・水 のオブジェクト
    [SerializeField]
    GameObject sand = null;
    GameObject sandObject;
    [SerializeField]
    GameObject water = null;
    GameObject waterObject;
    // 加える重りの位置
    [SerializeField]
    Transform scalePosition = null;
    // 聖杯内から溢れたことを判定するために扱うオブジェクト
    [SerializeField]
    GameObject DropObject = null;
    // Dropオブジェクトを生成する位置
    [SerializeField]
    Transform dropObjectPoint = null;
    // DropObjectの変数
    GameObject dropObject;
    DropObject dropObjectScript;
    // 聖杯の親オブジェクトの初期位置
    Vector3 parentInitialPosition;
    // 聖杯の親オブジェクトの初期
    Quaternion parentInitialQuaternion;
    // SE
    [SerializeField]
    SE se = null;
    // Chaliceが測りの中にあるかどうか
    public enum ChalicePosition
    {
        OutScale,
        InScale
    }
    // 現在の位置
    public ChalicePosition currentPosition;
    // プロパティ
    public Vector3 ParentInitialPosition
    {
        get {return parentInitialPosition;}
    }
    public Quaternion ParentInitialRotation
    {
        get {return parentInitialQuaternion;}
    }
    // 聖杯の中身の状態を変更するメソッド
    public void ChangeState(int num)
    {
        currentState = state[num];
    }
    // 聖杯の位置状態を変更するメソッド
    public void SetChalicePosition(ChalicePosition position)
    {
        currentPosition = position;
    }

    void Reset()
    {
        // DropGameObjectPointの取得
        dropObjectPoint = transform.GetChild(0);
        // SEの取得
        se = GetComponent<SE>();
    }
    // 重りに触れたら実行するメソッド
    public void AddWeight(float scale)
    {
        weight += scale;
    }
    // 聖杯の中に砂を生成するメソッド
    public void InstantiateSand()
    {
        sandObject = Instantiate(sand, scalePosition.position, Quaternion.identity) as GameObject;
        sandObject.transform.rotation = gameObject.transform.parent.rotation;
        sandObject.transform.parent = gameObject.transform;
    }
    // 聖杯の中に水を生成するメソッド
    public void InstantiateWater()
    {
        waterObject = Instantiate(water, scalePosition.position, Quaternion.identity) as GameObject;
        waterObject.transform.rotation = gameObject.transform.parent.rotation;
        waterObject.transform.parent = gameObject.transform;
    }
    // DropObjectを生成するメソッド
    void InstantiateDropObject()
    {
        dropObject = Instantiate(DropObject, dropObjectPoint.position, Quaternion.identity) as GameObject;
        dropObject.transform.parent = gameObject.transform;
    }
    // 逆さまにしたら追加した重りを削除するメソッド
    void DropScaleWeight()
    {
        // 空の状態・重さに変更
        ChangeState(0);
        weight = 1.0f;
        // 重りの削除
        foreach(Transform children in gameObject.transform)
        {
            if(children.gameObject.tag == "ScaleWeight")
            {
                Destroy(children.gameObject);
            }
        }
        // dropObjectの位置をリセット
        dropObject.transform.position = dropObjectPoint.position;
        if(containedScale == "Water")
        {
            // 水が落ちる音
            se.PlaySE(0, 0.4f);
        }
        if(containedScale == "Sand")
        {
            // 砂が落ちる音
            se.PlaySE(1, 1.0f);
        }
        // containeScaleの初期化
        containedScale = "";
    }
    void Start()
    {
        //初期の状態
        currentState = state[0];
        // DropObjectの生成・変数の初期化
        InstantiateDropObject();
        dropObjectScript = dropObject.GetComponent<DropObject>();
        // 初期の位置(測りの中にあるかどうか)
        SetChalicePosition(ChalicePosition.OutScale);
        // 親オブジェクトの初期位置・Quaternion 取得
        parentInitialPosition = transform.parent.position;
        parentInitialQuaternion = transform.parent.rotation;

    }

    void Update()
    {
        // 容器内が満たされている時,DropGameObjectが落ちたら実行する処理
        if(dropObjectScript.velocityY > 3.0f && currentState == state[1])
        {
            DropScaleWeight();
        }
    }

    // 聖杯を掴めない状態にするメソッド
    public void NoGrabbable()
    {
        transform.parent.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // ChaliceScaleの中にある時に扱うメソッド
    // DropObjectを停止状態にする
    public void StopDropObject()
    {
        dropObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    // DropObjectを動く状態にする
    public void MoveDropObject()
    {
        dropObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
