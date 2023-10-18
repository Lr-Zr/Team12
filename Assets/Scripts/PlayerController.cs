using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

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
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float fspeed = 10.0f;
    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard; //2번씩걸리는경우생김
        Managers.Input.KeyAction += OnKeyboard;//인풋매니저한테 어떤 키가 눌리면 이함수를 실행;

    }

    float fAngle = 0.0f;
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
    

    }


    void OnKeyboard()
    {
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
    }
}
