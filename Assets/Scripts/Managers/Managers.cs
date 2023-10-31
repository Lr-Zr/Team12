using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Managers : MonoBehaviour
{

    static Managers s_Instance;
    public static Managers Instance { get { Init(); return s_Instance; } }

    
    InputManager _input =new InputManager();
    ResourceManager _resource = new ResourceManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    



    void Start()
    {
        //Instance = this;
        Init();

        

    }
    void Update()
    {
        _input.OnUpdate();
    }


    static void Init()
    {
        if (s_Instance == null)
        {

            GameObject go = GameObject.Find("@Managers");//원본 이름;
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<Managers>();
        }
    }
}
