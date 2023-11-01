using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

/*
struct MyVector
{
    public float x;
    public float y;
    public float z;


    public float magnitude { get { return (float)Math.Sqrt(x * x + y * y + z * z); } } 
    public MyVector normalized { get { return new MyVector(x/magnitude, y/magnitude, z/magnitude ); } }   
    public MyVector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
    public static MyVector operator +(MyVector v1, MyVector v2)
    {
        return new MyVector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }
    public static MyVector operator -(MyVector v1, MyVector v2)
    {
        return new MyVector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }
    public static MyVector operator *(MyVector v1, float d)
    {
        return new MyVector(v1.x*d,v1.y*d,v1.z*d);
    }
    public static MyVector operator *( float d, MyVector v1)
    {
        return new MyVector(v1.x * d, v1.y * d, v1.z * d);
    }
}
*/
public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    float fspeed = 10.0f;


    Vector3 _destPos;
    bool _movetodest = false;

    PhotonView pv; 
    void Start()
    {
      //  pv= GetComponent<PhotonView>();
        Managers.Input.KeyAction -= OnKeyboard; //2번씩걸리는경우생김
        Managers.Input.KeyAction += OnKeyboard;//인풋매니저한테 어떤 키가 눌리면 이함수를 실행;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

    }
    float _waittorun;
    //float fAngle = 0.0f;
    void Update()
    {
     
        //fAngle += Time.deltaTime * fspeed;
        //Local->World
        //transform.TransformDirection
        //World->Local
        //transform.InverseTransfromDirection
        //Local
        //transform.Translate 


        //절대 회전 값 지정
        //  transform.eulerAngles = new Vector3(0.0f, fAngle, 0.0f);
        // +-delta 회전 값
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));
        //quaternion
        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, fAngle, 0.0f));

        if(_movetodest)
        {
            Vector3 dir = _destPos-transform.position;
            if(dir.magnitude<0.0001f)//크기가 매우 작아졌을때 도착했다는 상태
            {
                _movetodest=false;
            }else
            {
                //첫번째 방법
                //float moveDist = fspeed * Time.deltaTime;
                //if(moveDist>=dir.magnitude)
                //{
                //    moveDist = dir.magnitude;
                //}
                //<<<
                //두번째 방법
                float moveDist = Mathf.Clamp(fspeed * Time.deltaTime, 0, dir.magnitude);

                //<<<

                transform.position += dir.normalized * moveDist;
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(dir), 10*Time.deltaTime);
                transform.LookAt(_destPos);

            }
        }

        if(_movetodest)
        {
            _waittorun = Mathf.Lerp(_waittorun, 1,20.0f*Time.deltaTime);
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("wait_run_ratio", 1);
            anim.Play("WAIT_RUN");
        }
        else
        {
            _waittorun = Mathf.Lerp(_waittorun, 0, 20.0f * Time.deltaTime);
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("wait_run_ratio", 0);
             anim.Play("WAIT_RUN");
            
        }
    }


    void OnKeyboard()
    {
     //   if (!pv.IsMine) return;
        if (Input.GetKey(KeyCode.W))
        {

            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.5f);
            transform.position += Vector3.fwd * Time.deltaTime * fspeed;
            //transform.Translate(Vector3.fwd * Time.deltaTime * fspeed);

            //transform.position += transform.TransformDirection(new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {

            //transform.rotation = Quaternion.LookRotation(Vector3.back);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.5f);

            transform.position += Vector3.back * Time.deltaTime * fspeed;
            //transform.Translate(Vector3.fwd * Time.deltaTime * fspeed);
            // transform.position -= transform.TransformDirection(new Vector3(0.0f, 0.0f, 1.0f) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.5f);


            transform.position += Vector3.left * Time.deltaTime * fspeed;
            //transform.Translate(Vector3.fwd * Time.deltaTime * fspeed);
            // transform.position -= transform.TransformDirection(new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.D))
        {

            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.5f);

            transform.position += Vector3.right * Time.deltaTime * fspeed;
            //transform.Translate(Vector3.fwd * Time.deltaTime * fspeed);
            //transform.position += transform.TransformDirection(new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime);
        }
        _movetodest = false;
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.green, 3f);

        LayerMask mask = LayerMask.GetMask("Wall");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            _destPos = hit.point;
            _movetodest = true;
        }


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else
        {

        }
    }
}
