using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR
{
    // VR用のドアの開閉機能を管理するクラス
    public class DoorController : MonoBehaviour
    {
        // ドアの状態 開閉することが可能な状態であるか
        public enum State
        {
            Locked,
            UnLocked
        }

        public State CurrentState;

        [SerializeField] DoorCenterPoint _centerPoint = null;  // ドアの中心軸  何軸を基準に回転するか
        [SerializeField] DoorHandle _handle = null;            // ドアの取っ手
        [SerializeField] DoorHandPoint _handPoint = null;      // 手の位置に合わせて移動するオブジェクトのスクリプト
        [SerializeField] SE se = null;                  //  SE


        // This method runs whenever there are changes made to the serialized information in the editor.
        void OnValidate()
        {
            // ドアの状態と取っ手の状態を合わせる
            UpdateState(CurrentState);
        }

        void Start()
        {
            // event 追加
            ScaleDoorController.OnFixedStateChange += HandleFixedStateChange;
        }


        // 測りの扉が閉じたら event を発生させる.
        // この扉の状態を開閉可能な状態にする。
        void UpdateState(State state)
        {
            CurrentState = state;
            switch(CurrentState)
            {
                case State.Locked:
                    _handle.isLocked = true;
                    break;
                case State.UnLocked:
                    _handle.isLocked = false;
                    break;
                default:
                    break;
            }
        }

        // ドアを開閉可能な状態を変更するメソッド
        public void LockDoor()
        {
            UpdateState(State.Locked);
        }
        public void UnLockDoor()
        {
            UpdateState(State.UnLocked);
        }

        // ScaleDoorが固定状態になった時に行う処理
        void HandleFixedStateChange()
        {
            OpenDoor();
        }

        // ドアを開く処理
        void OpenDoor()
        {
            StartCoroutine(OpenDoorRoutine());
        }
        IEnumerator OpenDoorRoutine()
        {
            // 扉が開く音
            yield return new WaitForSeconds(1.0f);
            se.PlaySE(0, 0.9f);
            // 扉を２秒かけて30度開く処理
            float elapsedTime = 0;
            float anglerVelocity = 15.0f;
            float waitTime = 0.01f;
            while(elapsedTime <= 2.0f)
            {
                gameObject.transform.RotateAround(_centerPoint.transform.position, _centerPoint.AxisDirection, anglerVelocity * waitTime);
                elapsedTime += waitTime;
                yield return new WaitForSeconds(waitTime);
            }
            // 扉を開閉可能な状態に変更
            UnLockDoor();
            // 追加したeventを削除
            ScaleDoorController.OnFixedStateChange -= HandleFixedStateChange;
        }
    }
}