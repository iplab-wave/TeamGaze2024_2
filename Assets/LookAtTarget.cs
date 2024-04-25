using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// �^�[�Q�b�g�ɐU������X�N���v�g
/// </summary>
internal class LookAtTarget : MonoBehaviour
{
    // ���g��Transform
    [SerializeField] private Transform _self;

    // �^�[�Q�b�g��Transform
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
        //�v���J�n
        stopwatch.Restart();
        
    }


    private void Update()
    {
        //�������Ԃ�b�Ŏ擾
        float elapsed = (float)stopwatch.Elapsed.Milliseconds;

        print(elapsed);

        // �^�[�Q�b�g�̕����Ɏ��g����]������
        {   
            // _self��_target����������i����������j
            _self.LookAt(_target);

        }


        // ���̈ʒu�𒆐S�ɋ��ʏ�����C���ړ�
        {
            rot = _target.transform.eulerAngles;
            pos = _target.transform.position;
            objrot = transform.eulerAngles;


            // ���������̊p�x�Fphi
            if (rot.x > 180)
            {
                phi = (float)(Math.PI / 180) * (rot.x - 360) * -1;
            }
            else
            {
                phi = (float)(Math.PI / 180) * (rot.x * -1);
            }

            // ���������̊p�x�Ftheta
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