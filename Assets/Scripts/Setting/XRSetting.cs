using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// XRの設定
public class XRSetting : MonoBehaviour
{
    public float renderScale = 0.8f;    // デフォルトは1.0 上げれば上げるほどVR内の解像度が高くなる(その分描画は重くなる)
    void Start()
    {
        // 部屋を歩きまわることを想定したモード
        // Unityのカメラを配置した場所が、トラッキング空間の中央地面になる
        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
        // 解像度の変更
        XRSettings.eyeTextureResolutionScale = renderScale;
    }
}
