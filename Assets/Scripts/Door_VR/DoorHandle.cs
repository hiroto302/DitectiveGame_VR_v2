using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR
{
    // プレイヤーの手が取っ手に触れている時、その手の位置に合わせて Door(本体) を開閉させる機能
    public class DoorHandle : MonoBehaviour
    {
        private float dz = 0;         // 対辺の長さ
        private float dx;             // 隣辺の長さ
        private float radius;         // 半径の長さ
        private float deg;            // 角度
        private float posteriorAngle; // 移動後の角度

        // ドアに触れている手の位置のオブジェクト・その変数群
        [SerializeField] private GameObject handPoint = null;
        private Transform handPointTransform;
        private DoorHandPoint handPointScript;

        // 回転の中心点・軸方向
        [SerializeField] Transform centerPoint = null;
        private Vector3 rotationAxis;

        // 回転させるドアのオブジェクト
        [SerializeField] Transform door = null;

        // Handelに触れているか判定
        bool hasTouched = false;

        // Handのタグ名
        const string HandTag = "Hand";

        // 開閉可能な状態であるか
        public bool isLocked = true;

        void Reset()
        {
            // 一度親に移動してから取得 (複数のドアを作成した時、混合することを防ぐ）
            handPoint = transform.parent.Find("HandPoint").gameObject;
            centerPoint = transform.parent.Find("CenterPoint");
            door = transform.root;
        }

        void Start()
        {
            // HandPoint 変数の初期化
            handPointTransform = handPoint.transform;
            handPointScript = handPoint.GetComponent<DoorHandPoint>();
            // 半径の取得 初期化
            Vector3 dir = centerPoint.localPosition - handPointScript.InitialPosition;
            radius = dir.magnitude;
            // 隣辺の初期の長さ（最初半径と同じ長さ）
            dx = radius;
            // 回転軸の取得
            rotationAxis = centerPoint.GetComponent<DoorCenterPoint>().AxisDirection;
        }

        // ドアの取っ手に手が触れた時
        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag(HandTag) && !isLocked)
            {
                hasTouched = true;
            }
        }

        // Handの座標取得・移動条件
        void OnTriggerStay(Collider other)
        {
            if(other.gameObject.CompareTag(HandTag) && !isLocked)
            {
                // ワールド座標のHandの位置取得・HandPointの移動
                handPointTransform.position = other.gameObject.transform.position;
                // ドアの回転
                RotateDoor();
            }
        }

        // 取っ手から手が離れた時
        void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag(HandTag) && !isLocked)
            {
                hasTouched = false;
            }
        }

        // Door を 手の位置に合わせて、中心軸(今はY軸) を基準に回転させる
        public void RotateDoor()
        {
            if(hasTouched)
            {
                // 対辺の取得 handPointのローカル座標から取得
                dz = handPointTransform.localPosition.z - handPointScript.InitialPosition.z;
                // 隣辺を求める
                dx = Mathf.Sqrt(radius * radius - dz * dz);
                // オイラー角取得
                float rad = Mathf.Atan2(dz, dx);
                // オイラー角をラジアンに変換
                deg = rad * Mathf.Rad2Deg;
                // 手の位置により変化したdzにより、degを算出し、それにより生まれた角度分移動
                door.RotateAround( centerPoint.position, rotationAxis, deg);
                // 移動させた後、HandPointをリセットする
                handPointTransform.localPosition = handPointScript.InitialPosition;
            }
        }
    }
}
