//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EyeGazeController : MonoBehaviour
//{
//    public GameObject obj;
//    OVREyeGaze eyeGaze;

//    // スタート時に呼ばれる
//    void Start()
//    {
//        eyeGaze = GetComponent<OVREyeGaze>();
//    }

//    // フレーム更新毎に呼ばれる
//    void Update()
//    {
//        if (eyeGaze == null) return;

//        // アイトラッキングの有効時
//        if (eyeGaze.EyeTrackingEnabled)
//        {
//            // 視線の同期
//            obj.transform.rotation = eyeGaze.transform.rotation;
//        }
//    }
//}


//using UnityEngine;

//public class EyeGazeController : MonoBehaviour
//{
//    public GameObject obj;
//    public Transform centerEyeAnchor; // OVRCameraRigのCenterEyeAnchorへの参照

//    OVREyeGaze eyeGaze;

//    void Start()
//    {
//        eyeGaze = GetComponent<OVREyeGaze>();
//    }

//    void Update()
//    {
//        if (eyeGaze == null) return;

//        // 頭部の前方向に基づいて矢印の基本的な位置と向きを設定
//        Vector3 headPosition = centerEyeAnchor.position;
//        Vector3 headForward = centerEyeAnchor.forward;

//        // 矢印を頭部の前方に配置する
//        obj.transform.position = headPosition + headForward * 0.5f; // ここでの距離(0.5m)は調整可能

//        // 視線追跡が有効な場合、視線の方向に微調整
//        if (eyeGaze.EyeTrackingEnabled)
//        {
//            Vector3 gazeDirection = eyeGaze.transform.forward;
//            obj.transform.rotation = Quaternion.LookRotation(gazeDirection);
//        }
//        else
//        {
//            // 視線追跡が無効な場合は、頭部の向きに矢印を合わせる
//            obj.transform.rotation = Quaternion.LookRotation(headForward);
//        }
//    }
//}


using UnityEngine;

public class EyeGazeController : MonoBehaviour
{
    public GameObject obj;
    public Transform centerEyeAnchor; // OVRCameraRigのCenterEyeAnchorへの参照
    public float Distance = 0.5f; // 頭からオブジェクトまでの距離

    OVREyeGaze eyeGaze;

    void Start()
    {
        eyeGaze = GetComponent<OVREyeGaze>();
    }

    void Update()
    {
        if (eyeGaze == null || !eyeGaze.EyeTrackingEnabled) return;

        // 視線の方向を取得
        Vector3 gazeDirection = eyeGaze.transform.forward;

        // 視線方向から球面座標を計算
        float phi = Mathf.Asin(gazeDirection.y);
        float theta = Mathf.Atan2(gazeDirection.x, gazeDirection.z);

        // 球面座標からワールド座標に変換
        Vector3 objPosition = new Vector3(
            Distance * Mathf.Cos(phi) * Mathf.Sin(theta),
            Distance * Mathf.Sin(phi),
            Distance * Mathf.Cos(phi) * Mathf.Cos(theta)
        );

        // 頭の位置を基準にオブジェクトの位置を設定
        obj.transform.position = centerEyeAnchor.position + objPosition;

        // オブジェクトを中心点に向ける
        obj.transform.LookAt(centerEyeAnchor.position);
    }
}