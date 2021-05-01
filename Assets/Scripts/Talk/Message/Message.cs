using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

// 指定した文字を、Panelのtextに表示するクラス
// SetMessagePanelメソッドで、SetMessageに指定したルール(splitStringなど)で表示したい文字列を記述
// MessageStartでセットした文字の表示開始
public class Message : MonoBehaviour
{
    // メッセージUI
    private Text messageText;
    // 表示するメッセージ
    [SerializeField]
    // TextAreaAttribute テキストアトリビュートはインスペクタで最低1行で表示10行にし、それより多い分はスクロールして見れるようにしている
    [TextArea(1, 10)]
    private string allMessage =
            "一番目\n"
            + "Text入力の例\n<>"
            + "allMessageの2番目に表示されるもの";

    // 使用する分割文字列
    // splitStringは全会話内容を1回で表示するメッセージに分割する時の分割文字列を設定。ここのデフォルでは<>。セパレーターの役割をするもの
    [SerializeField]
    public string splitString = "<>";
    // allMessageを<>で分割したメッセージ
    private string[] splitMessage;
    // 分割したメッセージが何番目のものであるか
    private int messageNum;
    // テキストスピード
    [SerializeField]
    private float textSpeed = 0.05f;
    // 経過時間 (次のメッセージを表示するまでの経過時間やアイコンの点滅の経過時間で扱う)
    private float elapsedTime = 0f;
    // 現在表示しているmessageにおいて、今見ている文字が何番目であるか表す番号
    private int nowTextNum = 0;
    // マウスクリックを促すアイコン
    private Image clickIcon;
    // クリックアイコンの点滅秒数
    [SerializeField]
    private float clickFlashTime = 0.2f;
    // 1回分のメッセージを表示したかどうか
    private bool isOneMessage = false;
    // メッセージをすべて表示したかどうか
    private bool isEndMessage = false;

    // メッセージスタート
    public bool isStartMessage = false;

    // SE
    [SerializeField]
    SE se = null;

    void Start()
    {
        // クリックアイコンの取得・非表示
        clickIcon = transform.Find("MessagePanel/Image").GetComponent<Image>();
        clickIcon.enabled = false;
        // Textの取得
        messageText = transform.Find("MessagePanel/Text").GetComponent<Text>();
        messageText.text = "";
        // Panelを非表示
        transform.GetChild(0).gameObject.SetActive(false);

        // SE の取得
        if(se != null)
        {
            se = GetComponent<SE>();
        }
    }

    void Update()
    {
        // 会話状態でない時、会話が設定されていない時、これ以降の処理を実行しない
        if(isStartMessage == false)
        {
            return;
        }
        // 会話スタート
        else if(isStartMessage ==true)
        {
            // メッセージが終わっているか、メッセージがない場合
            // 会話終了
            if (isEndMessage || allMessage == null)
            {
                isStartMessage =  false;
                MessageEnd();
                return;
            }
            // 1回に表示するメッセージを表示しきれていない時
            if (!isOneMessage)
            {
                // テキスト表示時間を経過したらメッセージを追加
                if (elapsedTime >= textSpeed)
                {
                    // 現在までに表示されている文字列に、対応した文字を1つずつ足していく
                    messageText.text += splitMessage[messageNum][nowTextNum];

                    // 値の更新
                    nowTextNum++;
                    elapsedTime = 0f;

                    // メッセージを全部表示、または行数が最大数表示された
                    if (nowTextNum >= splitMessage[messageNum].Length)
                    {
                        isOneMessage = true;
                    }
                }
                elapsedTime += Time.deltaTime;

                // メッセージ表示中にマウスの左ボタン or Aボタン(オキュラスクエスト) を押したら一括表示
                if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.One) )
                {
                    // ここまでに表示しているテキストに残りのメッセージを足す
                    // Stringクラス Substringメソッドを利用し、残りの文字を抜き出すして追加する
                    messageText.text += splitMessage[messageNum].Substring(nowTextNum);
                    isOneMessage = true;
                }
            }
            // 1回に表示するメッセージを表示した後の処理
            else
            {

                elapsedTime += Time.deltaTime;

                // クリックアイコンを点滅する時間を超えた時、反転させる
                if (elapsedTime >= clickFlashTime)
                {
                    clickIcon.enabled = !clickIcon.enabled;
                    elapsedTime = 0f;
                }

                //マウスクリック or Aボタンが押されたら次の文字表示処理の準備
                if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.One))
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    elapsedTime = 0f;
                    isOneMessage = false;
                    // クリック音再生
                    se.PlaySE(0, 1.0f);

                    // メッセージが全部表示された後の処理。ゲームオブジェクト自体を非表示にする
                    if (messageNum >= splitMessage.Length)
                    {
                        isEndMessage = true;
                        transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    // 新しいメッセージの設定
    // 全ての会話文を引数で受け取り、それを1回で表示するメッセージに分割して配列にします
    void SetMessage(string message)
    {
        this.allMessage = message;
        // 分割文字列で一回に表示するメッセージを分割
        // Regex.Splitメソッド使用し、allMessage(第一引数)を正規表現パターン(第二引数)で分割し、戻り値であるstringの配列を得る
        // 分割文字列はsplitStringの文字と前後に\s*を付けて『空白文字列splitString空白文字列』というパターンを分割文字列とする
        // \s* : 0個以上の空白文字と一致
        // 第３引数のオプションで空白無視無視
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        isOneMessage = false;
        isEndMessage = false;
    }

    // 他のスクリプトから新しいメッセージを設定しUIをアクティブにする
    public void SetMessagePanel(string message)
    {
        SetMessage(message);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    // 下記の2つの処理は、このクラスを継承したもので,メッセージ開始と終わり時に行いたい処理を記述する
    // message の表示開始
    public virtual void MessageStart()
    {
        isStartMessage = true;
    }
    // messageが終了後の処理
    public virtual void  MessageEnd()
    {
    }
}
