using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hittest : MonoBehaviour
{
    [SerializeField]
    float gauge = 0.1f;


    private void Update()
    {
        Managers.Input.KeyAction -= OnKeyboard; //2번씩걸리는경우생김
        Managers.Input.KeyAction += OnKeyboard;//인풋매니저한테 어떤 키가 눌리면 이함수를 실행;

    }
    Vector3 dir;
    void OnKeyboard()
    {
        //   if (!pv.IsMine) return;
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("위치초기화");
            this.transform.position = new Vector3(5, 1, 0);
        }
        if (Input.GetKey(KeyCode.J))
        {
            Debug.Log("45");
            dir = new Vector3(1f, 1f, 0f);
        }
        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("60");
            dir = new Vector3(1f, Mathf.Sqrt(3), 0f);
        }
        if (Input.GetKey(KeyCode.L))
        {
            Debug.Log("30");
            dir = new Vector3(Mathf.Sqrt(3), 1f, 0f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {

            float force = collision.collider.transform.GetComponent<PlayerController1>().power * gauge;
            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.AddForce(dir * force, ForceMode.Impulse);
        }
    }
}
