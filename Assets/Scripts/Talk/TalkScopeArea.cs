using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// キャラクター(Cat)の任意の範囲にPlayerが近ずいた時、話すIconを表示するスクリプト
public class TalkScopeArea : MonoBehaviour
{
    // アイコンを表示しているか判定
    protected bool showIcon = false;
    // 表示・非表示にする会話アイコン
    [SerializeField]
    protected GameObject talkIcon = null;
    // プレイヤーの変数群
    [SerializeField]
    protected GameObject player = null;
    protected Transform playerTarnsform;
    protected PlayerController playerController;
    protected float rotationSpeed = 1.0f;
    // Playerのタグ名
    const string PlayerTag = "Player";

    void Reset()
    {
        // Talk_IconをTalkScopeAreaの子(0番目にセット)
        talkIcon = transform.GetChild(0).gameObject;
        // Playerの取得
        player = GameObject.Find("Player");
    }
    void Awake()
    {
        // 初期は非表示
        talkIcon.SetActive(false);
        // Player 変数の初期化
        playerTarnsform = player.GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(PlayerTag))
        {
            showIcon = true;
            // アイコンを表示
            talkIcon.SetActive(true);
            // アイコンを表示する音
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(PlayerTag))
        {
            showIcon = false;
            // アイコンを非表示
            talkIcon.SetActive(false);
        }
    }

    void Update()
    {
        // アイコンを表示中のみ下記の処理を実行
        if(showIcon == true)
        {
            IconDirection();
        }
    }
    // アイコンの方向をPlayerに合わせる
    protected void IconDirection()
    {
            // Playerの位置の方向にアイコンの向きわ合わせる
            // form(第一引数)からto(第二引数)の間の角度を算出 yの位置をどちらかのオブジェクトに合わせる
            // float f = Vector3.Angle(transform.forward, new Vector3(playerTarnsform.position.x, transform.position.y, playerTarnsform.position.z) - transform.position);
            // LookRotation により、あるオブジェクトから見た別のオブジェクトの方向ベクトルを引数に渡すことで、そのベクトルに対するQuaternion型を取得
            // Quaternion.Lerp により、現在位置(第一引数)から第二引数に向けて徐々に回転させていく
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(playerTarnsform.position.x, transform.position.y, playerTarnsform.position.z) - transform.position), rotationSpeed * Time.deltaTime);
    }
}
