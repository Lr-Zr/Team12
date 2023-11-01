using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuerterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);//플레이어와 카메라 거리


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
                //플레이어에서 카메라로 raycast 발사 
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                //강제로 한곳을 주시
                transform.LookAt(_player.transform.position);
                //덜덜 떨리는 현상 . 카메라가 이동, 플레이어가 이동 차이. -> 해결법 LateUpdate()
            }


        }
      
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuerterView; _delta = delta;
    }
}
