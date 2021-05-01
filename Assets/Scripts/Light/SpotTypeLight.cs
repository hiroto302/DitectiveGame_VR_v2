using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SpotTypeLightを操作するクラス
public class SpotTypeLight : MonoBehaviour
{
    [SerializeField]
    Light light = null;
    // intensityの初期値
    public float initialIntensity;
    // 暗く点滅し
    void Reset()
    {
        light = GetComponent<Light>();
        initialIntensity = light.intensity;
    }

}
