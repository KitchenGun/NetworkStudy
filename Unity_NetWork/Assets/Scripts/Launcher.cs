using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    [Tooltip("방의 최대 인원수")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private GameObject progressLabel;


    private bool isConnecting;


    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;//자동 씬 동기화 true
    }
    private void Start()
    {
        //Connect();
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void Connect()
    {
        isConnecting = true;

        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
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
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {//연결해제
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("연결 해제 {0}", cause);
    }

    public override void OnJoinedRoom()
    {//방에 입장
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("방1에 합류했습니다.");
            PhotonNetwork.LoadLevel("Room for 1");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {//랜덤방을 못 찾을 경우
        Debug.Log("랜덤방이 없음");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers=maxPlayersPerRoom});
    }


}
