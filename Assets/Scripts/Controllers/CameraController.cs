using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuerterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);//�÷��̾�� ī�޶� �Ÿ�


    [SerializeField]
    GameObject _player;


    void Start()
    {
        //_player =PhotonNetwork.Instantiate("Prefabs/unitychan", Vector3.zero, Quaternion.identity);
        
    }


    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuerterView)
        {
            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                //�÷��̾�� ī�޶�� raycast �߻� 
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                //������ �Ѱ��� �ֽ�
                transform.LookAt(_player.transform.position);
                //���� ������ ���� . ī�޶� �̵�, �÷��̾ �̵� ����. -> �ذ�� LateUpdate()
            }


        }
      
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuerterView; _delta = delta;
    }
}
