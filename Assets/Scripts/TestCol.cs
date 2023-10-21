using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCol : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision : {collision.gameObject.name}");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger : {other.name}");
    }
    void Start()
    {

    }


    void Update()
    {
        //    Vector3 look = transform.TransformDirection(Vector3.forward);
        //    Debug.DrawRay(transform.position + Vector3.up, Vector3.forward * 10f, Color.red);
        //    Debug.DrawRay(transform.position + Vector3.up, look * 10f, Color.red);
        //    RaycastHit hit;
        //    RaycastHit[] hits;

        //    hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);
        //    if (Physics.Raycast(transform.position + Vector3.up, Vector3.forward, out hit, 10))
        //        if (Physics.Raycast(transform.position + Vector3.up, look, out hit, 10))
        //        {
        //            Debug.Log($"Raycast : {hit.collider.name}");
        //        }

        //    foreach (RaycastHit ohit in hits)
        //    {
        //        Debug.Log($"RaycastAll: {ohit.collider.gameObject.name}");
        //    }
        //}

        //Debug.Log(Input.mousePosition);

        // Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition)); //viewport 좌표
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.green, 3f);

            int mask = (1 << 8) + (1<<9); //레이어마스크
            LayerMask mask = LayerMask.GetMask("Monster");
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f,mask))
            {
                if (hit.collider != null)
                    Debug.Log($"from camera  : {hit.collider.name}");
            }
        }
    }
    //if (Input.GetMouseButtonDown(0))
    //{

    //    Vector3 mouserWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    //    Vector3 dir = mouserWorldPosition - Camera.main.transform.position;// 벡터방향
    //    dir = dir.normalized;
    //    Debug.Log("world "+mouserWorldPosition);
    //    Debug.DrawRay(Camera.main.transform.position, dir*100f, Color.green,3f);
    //    RaycastHit hit;
    //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit,100f)) ;
    //    {
    //        if(hit.collider!=null)
    //        Debug.Log($"from camera  : {hit.collider.name}");
    //    }
    //}

}
