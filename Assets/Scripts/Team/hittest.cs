using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hittest : MonoBehaviour
{
    [SerializeField]
    float gauge = 0.1f;

    Rigidbody body;

    private void Update()
    {
        Managers.Input.KeyAction -= OnKeyboard; //2번씩걸리는경우생김
        Managers.Input.KeyAction += OnKeyboard;//인풋매니저한테 어떤 키가 눌리면 이함수를 실행;
        body=GetComponent<Rigidbody>();
    }
    Vector3 dir;

    void OnKeyboard()
    {
        //   if (!pv.IsMine) return;
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("위치초기화");
            body.velocity = Vector3.zero;
            this.transform.position = new Vector3(5, 1, 0);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {

            Vector3 tt = collision.collider.transform.GetComponent<PlayerController1>()._powervector * gauge / 100f;
            Rigidbody rigid = GetComponent<Rigidbody>();
            rigid.AddForce(tt, ForceMode.Impulse);
        }
    }
}
