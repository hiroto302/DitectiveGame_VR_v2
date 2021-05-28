using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    // Catの状態を表す State型 の列挙体
    public enum State
    {
        Normal,
        Talk
    }
    // Catの現在の状態
    public State currentState;
    // 振り向く速度
    float rotationSpeed = 1.0f;
    // 振り向く相手(Player)
    [SerializeField]
    Transform playerTransfrom = null;
    // playerのスクリプト
    [SerializeField]
    PlayerController playerController = null;

    // animation
    Animator animator;
    // 経過時間
    float elapsedTime = 0;
    // 現在向いてる方向
    float currentDirection;
    // 一定時間経過した時向いてる方向
    float nextDirection;
    void Reset()
    {
        playerTransfrom = GameObject.Find("Player").GetComponent<Transform>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Awake()
    {
        // animatorの取得
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        // 状態の初期化
        currentState = State.Normal;

        // Playerの状態が変更された時の, event の追加
        playerController.onStateUpdate += HandlePlayerStateUpdate;
    }

    void Update()
    {
        // 猫の状態をPlayerと同様にする
        // if(playerController.currentState == PlayerController.State.Normal && currentState != State.Normal)
        // {
        //     SetState(State.Normal);
        // }
        // else if(playerController.currentState == PlayerController.State.Talk && currentState != State.Talk)
        // {
        //     SetState(State.Talk);
        // }

        // // 話す状態になる時行う処理
        // if(currentState == State.Talk)
        // {
        //     // Playerの方向を向かせる
        //     transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(playerTransfrom.position.x, transform.position.y, playerTransfrom.position.z) - transform.position), rotationSpeed * Time.deltaTime);

        //     // 振り向きのアニメーション制御
        //     if(elapsedTime < 0.2f)
        //     {
        //         currentDirection = transform.localEulerAngles.y;
        //     }
        //     elapsedTime += Time.deltaTime;
        //     if(elapsedTime > 0.2f)
        //     {
        //         nextDirection = transform.localEulerAngles.y;
        //         elapsedTime = 0;
        //     }
        //     if(Mathf.Abs(currentDirection - nextDirection) > 0.05f)
        //     {
        //         animator.SetBool("TurnAround", true);
        //     }
        //     else
        //     {
        //         animator.SetBool("TurnAround", false);
        //     }
        // }
        TurnToPlayer();
    }

    // 状態を変更するメソッド
    public void SetState(State state)
    {
        currentState = state;
        // アニメーションの処理記述
        if(state == State.Normal)
        {
            animator.SetBool("Talk", false);
        }
        else if(state == State.Talk)
        {
            animator.SetBool("Talk", true);
        }
    }

    // Playerの状態が変わった時に行う処理
    void HandlePlayerStateUpdate(PlayerController.State playerState)
    {
        if(playerState == PlayerController.State.Normal)
        {
            SetState(State.Normal);
        }
        else if (playerState == PlayerController.State.Talk)
        {
            SetState(State.Talk);
        }
    }

    // 会話状態になった時,Playerの方向を向く処理
    void TurnToPlayer()
    {
        // 話す状態になる時行う処理
        if(currentState == State.Talk)
        {
            // Playerの方向を向かせる
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(playerTransfrom.position.x, transform.position.y, playerTransfrom.position.z) - transform.position), rotationSpeed * Time.deltaTime);

            // 振り向きのアニメーション制御
            if(elapsedTime < 0.2f)
            {
                currentDirection = transform.localEulerAngles.y;
            }
            elapsedTime += Time.deltaTime;
            if(elapsedTime > 0.2f)
            {
                nextDirection = transform.localEulerAngles.y;
                elapsedTime = 0;
            }
            if(Mathf.Abs(currentDirection - nextDirection) > 0.05f)
            {
                animator.SetBool("TurnAround", true);
            }
            else
            {
                animator.SetBool("TurnAround", false);
            }
        }
    }
}
