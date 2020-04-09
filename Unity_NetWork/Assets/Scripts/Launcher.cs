using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";


    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;//자동 씬 동기화 true
    }
    private void Start()
    {
        Connect();
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {//연결
        Debug.Log("마스터연결");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {//연결해제
        Debug.LogWarningFormat("연결 해제 {0}", cause);
    }

    public override void OnJoinedRoom()
    {//방에 입장
        Debug.Log("방에 합류했습니다.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {//랜덤방을 못 찾을 경우
        Debug.Log("랜덤방이 없음");
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }
}
