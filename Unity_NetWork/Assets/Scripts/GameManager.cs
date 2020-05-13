using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    private void Start()
    {
        Instance = this;
        if(PlayerManager.LocalPlayerInstance == null)//playerPrefab==null)
        {
            Debug.Log("Player Prefab missing");
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
        else
        {
            Debug.LogFormat("Loaded Level Name{0}", Application.loadedLevelName);
            //PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
    }


    public override void OnLeftRoom()
    {//방을 떠날때
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void LoadArena()
    {//방생성
        if(!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("마스터 클라이언트가 아닙니다");
        }
        Debug.LogFormat("로딩레벨{0}",PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);//인원수에 따른 방 불러오기 (포톤제공)

    }
    public override void OnPlayerEnteredRoom(Player other)
    {//플레이어가 들어왔을 경우
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);
        if(PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {//플레이어가 방을 나갔을 경우
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);
        if (PhotonNetwork.IsMasterClient)//호스트인지확인
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }
}
