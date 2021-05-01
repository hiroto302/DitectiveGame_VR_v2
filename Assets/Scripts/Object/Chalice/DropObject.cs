using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    Rigidbody rb;
    // 下方向の力を取得する
    public float velocityY;
    // Chaliceの変数
    Chalice chaliceScript;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        // Caliceの状態取得
        chaliceScript = transform.root.gameObject.GetComponentInChildren<Chalice>();
    }

    void Update()
    {
        if(chaliceScript.currentState == "fill")
        {
            // rb.isKinematic = false;
            velocityY = Mathf.Abs(rb.velocity.y);
        }
        else
        {
            velocityY = 0;
            rb.isKinematic = true;
        }
    }

    public void ChangeIsKinematic()
    {
        if(!rb.isKinematic)
        {
            rb.isKinematic = true;
        }
        else if(rb.isKinematic)
        {
            rb.isKinematic = false;
        }
    }
}
