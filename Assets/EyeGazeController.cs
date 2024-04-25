//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EyeGazeController : MonoBehaviour
//{
//    public GameObject obj;
//    OVREyeGaze eyeGaze;

//    // �X�^�[�g���ɌĂ΂��
//    void Start()
//    {
//        eyeGaze = GetComponent<OVREyeGaze>();
//    }

//    // �t���[���X�V���ɌĂ΂��
//    void Update()
//    {
//        if (eyeGaze == null) return;

//        // �A�C�g���b�L���O�̗L����
//        if (eyeGaze.EyeTrackingEnabled)
//        {
//            // �����̓���
//            obj.transform.rotation = eyeGaze.transform.rotation;
//        }
//    }
//}


//using UnityEngine;

//public class EyeGazeController : MonoBehaviour
//{
//    public GameObject obj;
//    public Transform centerEyeAnchor; // OVRCameraRig��CenterEyeAnchor�ւ̎Q��

//    OVREyeGaze eyeGaze;

//    void Start()
//    {
//        eyeGaze = GetComponent<OVREyeGaze>();
//    }

//    void Update()
//    {
//        if (eyeGaze == null) return;

//        // �����̑O�����Ɋ�Â��Ė��̊�{�I�Ȉʒu�ƌ�����ݒ�
//        Vector3 headPosition = centerEyeAnchor.position;
//        Vector3 headForward = centerEyeAnchor.forward;

//        // ���𓪕��̑O���ɔz�u����
//        obj.transform.position = headPosition + headForward * 0.5f; // �����ł̋���(0.5m)�͒����\

//        // �����ǐՂ��L���ȏꍇ�A�����̕����ɔ�����
//        if (eyeGaze.EyeTrackingEnabled)
//        {
//            Vector3 gazeDirection = eyeGaze.transform.forward;
//            obj.transform.rotation = Quaternion.LookRotation(gazeDirection);
//        }
//        else
//        {
//            // �����ǐՂ������ȏꍇ�́A�����̌����ɖ������킹��
//            obj.transform.rotation = Quaternion.LookRotation(headForward);
//        }
//    }
//}


using UnityEngine;

public class EyeGazeController : MonoBehaviour
{
    public GameObject obj;
    public Transform centerEyeAnchor; // OVRCameraRig��CenterEyeAnchor�ւ̎Q��
    public float Distance = 0.5f; // ������I�u�W�F�N�g�܂ł̋���

    OVREyeGaze eyeGaze;

    void Start()
    {
        eyeGaze = GetComponent<OVREyeGaze>();
    }

    void Update()
    {
        if (eyeGaze == null || !eyeGaze.EyeTrackingEnabled) return;

        // �����̕������擾
        Vector3 gazeDirection = eyeGaze.transform.forward;

        // �����������狅�ʍ��W���v�Z
        float phi = Mathf.Asin(gazeDirection.y);
        float theta = Mathf.Atan2(gazeDirection.x, gazeDirection.z);

        // ���ʍ��W���烏�[���h���W�ɕϊ�
        Vector3 objPosition = new Vector3(
            Distance * Mathf.Cos(phi) * Mathf.Sin(theta),
            Distance * Mathf.Sin(phi),
            Distance * Mathf.Cos(phi) * Mathf.Cos(theta)
        );

        // ���̈ʒu����ɃI�u�W�F�N�g�̈ʒu��ݒ�
        obj.transform.position = centerEyeAnchor.position + objPosition;

        // �I�u�W�F�N�g�𒆐S�_�Ɍ�����
        obj.transform.LookAt(centerEyeAnchor.position);
    }
}