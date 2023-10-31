using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    string _gameVersion = "1";
    string _userID = "1";
    [SerializeField]
    Text _connectionInfo;
    [SerializeField]
    Button _joinBtn;
    void Start()
    {
        /* ���ӿ� �ʿ��� ���� ���� ���� */
        PhotonNetwork.GameVersion = _gameVersion;
        /* ���� ���� ���� �õ� */
        PhotonNetwork.ConnectUsingSettings();
        /* �� ���� ��ư ��Ȱ�� */
        _joinBtn.interactable = false;
        _connectionInfo.text = "���� ������ ������ .. ";

    }

    /* ������ ������ ���ӽ� �ڵ� ���� */
    public override void OnConnectedToMaster()
    {
        /*��ư Ȱ��*/
        _joinBtn.interactable = true;
        _connectionInfo.text = "on : ���� ���� ���� �Ϸ�";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _joinBtn.interactable = false;
        _connectionInfo.text = "off : ���� ����... �翬�� �õ���..";

        PhotonNetwork.ConnectUsingSettings();
    }


    /* �濡 ������ �õ� */
    public void Connect()
    {
        /* Ŭ�� �� �ߺ� �õ� ���� */
        _joinBtn.interactable = false;
        if (PhotonNetwork.IsConnected)
        {
            _connectionInfo.text = "�濡 ������..";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            _joinBtn.interactable = false;
            _connectionInfo.text = "off : ���� ����... �翬�� �õ���..";

            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /* ���� ����� ���� ��� �� ���� ������ ���� �ִ� 2�� */
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        _connectionInfo.text = "���ο� �� ������ ..";
        Debug.Log($"�� ���� �� ");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });

    }

    /* �� ���� ���� */
    public override void OnJoinedRoom()
    {
        /* �� ���� ���� ǥ�� */
        _connectionInfo.text = "�� ���� ���� ";

        /* Scene ���� */
        PhotonNetwork.LoadLevel("Test1");
        //PhotonNetwork.Instantiate("Prefabs/unitychan", Vector3.zero, Quaternion.identity);

    }
}
