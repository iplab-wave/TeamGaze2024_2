using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// ターゲットに振り向くスクリプト
/// </summary>
internal class LookAtTarget : MonoBehaviour
{
    // 自身のTransform
    [SerializeField] private Transform _self;

    // ターゲットのTransform
    [SerializeField] private Transform _target;

    //public Transform centerEyeAnchor;
    OVREyeGaze eyeGaze;

    //public GameObject center;
    public float Distance = (float)0.5;

    private Vector3 obj;
    private Vector3 objrot;

    private Vector3 rot;
    private Vector3 pos;

    private float phi;
    private float theta;
    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

    void Start()
    {
        eyeGaze = GetComponent<OVREyeGaze>();
        //計測開始
        stopwatch.Restart();
        
    }


    private void Update()
    {
        //処理時間を秒で取得
        float elapsed = (float)stopwatch.Elapsed.Milliseconds;

        print(elapsed);

        // ターゲットの方向に自身を回転させる
        {   
            // _selfが_targetを見続ける（向き続ける）
            _self.LookAt(_target);

        }


        // 頭の位置を中心に球面状をレイが移動
        {
            rot = _target.transform.eulerAngles;
            pos = _target.transform.position;
            objrot = transform.eulerAngles;


            // 垂直向きの角度：phi
            if (rot.x > 180)
            {
                phi = (float)(Math.PI / 180) * (rot.x - 360) * -1;
            }
            else
            {
                phi = (float)(Math.PI / 180) * (rot.x * -1);
            }

            // 水平方向の角度：theta
            theta = (float)(Math.PI / 180) * rot.y;

            //rot.z = 

            obj.x = Distance * (float)Math.Cos(phi) * (float)Math.Sin(theta) + pos.x;
            obj.y = Distance * (float)Math.Sin(phi) + pos.y -0.02f;
            obj.z = Distance * (float)Math.Cos(phi) * (float)Math.Cos(theta) + pos.z;
            objrot.z = rot.z;


            transform.position = obj;
            transform.eulerAngles = objrot;
        
        }



    }

    bool range(float a, float b, float c)
    {
        if (b <= a && a <= c)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}