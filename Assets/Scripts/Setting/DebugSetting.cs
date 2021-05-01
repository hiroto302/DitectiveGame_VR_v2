using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// テストPlayが終了したらfalseにすること
public class DebugSetting : MonoBehaviour
{
    // OVRDebugConsole.Logメソッド実行時に、その内容を表示しているオブジェクト
    [SerializeField]
    GameObject OVRDebugConsoleObject = null;
    [SerializeField]
    GameObject OVRDebugConsoleOutputObject = null;
    // playerの操作
    [SerializeField]
    DebugController debugControllerScript = null;

    public bool testPlay = true;

    void Reset()
    {
        // 参照先のの取得
        OVRDebugConsoleObject = GameObject.Find("OVRDebugConsole");
        OVRDebugConsoleOutputObject = GameObject.Find("OVRDebugConsoleOutput");
        debugControllerScript = GameObject.Find("Player").GetComponent<DebugController>();
    }

    void Start()
    {
        TestPlay(testPlay);
    }

    void TestPlay(bool testPlay)
    {
        // Debug.Log で出力しているものの表示・非表示
        Debug.unityLogger.logEnabled = testPlay;
        // OVRDebugConsole.Logの表示・非表示
        OVRDebugConsoleObject.SetActive(testPlay);
        OVRDebugConsoleOutputObject.SetActive(testPlay);
        // playerの操作無効
        debugControllerScript.enabled = testPlay;
    }
}
