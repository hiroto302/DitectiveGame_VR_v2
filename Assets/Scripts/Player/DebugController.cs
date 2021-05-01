using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    Rigidbody rb;
    private float angleSpeed = 45.0f;
    private float moveSpeed = 2.25f;
    float x = 0;
    float z = 0;
    [SerializeField]
    Transform moveTarget = null;

    void Reset()
    {
        if(!moveTarget)
        {
            moveTarget = GetComponentInChildren<OVRCameraRig>().transform.Find("TrackingSpace/CenterEyeAnchor");
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, 1.0f, 0) * angleSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -1.0f, 0) * angleSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            x = 1.0f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            x = -1.0f;
        }
        else
        {
            x = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            z = 1.0f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            z = -1.0f;
        }
        else
        {
            z = 0;
        }

        Vector3 move = (x * moveTarget.transform.right.normalized  + z * moveTarget.transform.forward.normalized  ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }
}
