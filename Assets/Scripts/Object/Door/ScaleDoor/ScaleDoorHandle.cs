using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScaleDoorに対応した機能を実装する
// 横軸に中心に回転
// このスクリプトではPlayerが直感的にDoorの開閉が可能だが、
// 操作性をより簡易的にするためにScaleDoorControllerで実装を試みる
public class ScaleDoorHandle : DoorHandle
{
    void Start()
    {
        // HandPoint 変数の初期化
        handPointTransform = handPoint.transform;
        handPointScript = handPoint.GetComponent<HandPoint>();
        // 半径の取得 初期化
        Vector3 dir = centerPoint.localPosition - handPointTransform.localPosition;
        radius = dir.magnitude;
        // 隣辺の初期の長さ（最初半径と同じ長さ）
        dx = radius;
    }
    void Update()
    {
        // 対辺の取得 handPointのローカル座標から取得
        dz = handPointTransform.localPosition.x - handPointScript.InitialPosition.x;
        // 隣辺を求める
        dx = Mathf.Sqrt(radius * radius - dz * dz);
        // オイラー角取得
        float rad = Mathf.Atan2(dz, dx);
        // オイラー角をラジアンに変換
        deg = rad * Mathf.Rad2Deg;
        // 手の位置により変化したdzにより、degを算出し、それにより生まれた角度分移動
        door.RotateAround( centerPoint.position, door.forward.normalized, deg * 1.0f);
        // 移動させた後、HandPointをリセットする
        handPointTransform.localPosition = handPointScript.InitialPosition;
        if(Input.GetKeyDown(KeyCode.O))
        {
            // door.RotateAround( centerPoint.position, Vector3.forward, 15.0f * 1.0f);
            door.RotateAround( centerPoint.position, door.forward.normalized, 15.0f * 1.0f);
        }
    }
}
