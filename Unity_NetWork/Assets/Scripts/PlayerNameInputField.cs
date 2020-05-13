using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;


[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    const string playerNamePrefKey = "PlatyerName";//플레이어 이름
    private InputField _inputField;

    private void Start()
    {
        string defaultName = string.Empty;
        _inputField = this.GetComponent<InputField>();//인풋 필드 가져오기
        if((_inputField!=null))
        {
            if(PlayerPrefs.HasKey(playerNamePrefKey))//사용자의 키값이 있는지 확인
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);//Prefab에 키값을 저장
                _inputField.text = defaultName;
            }
        }
        PhotonNetwork.NickName = defaultName;//네트워크 구별용 닉네임으로 저장
    }

    public void SetPlayerName()//내용이 변경될때마다 이 함수를 호출
    {
        string value = _inputField.text;
        if(string.IsNullOrEmpty(value))
        {
            Debug.LogError("name is empty");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);//닉네임 변경
    }

    private void OnDestroy()
    {
        Debug.Log("PhotonNetwork.Nickname " + PhotonNetwork.NickName);//포톤 서버에 저장된 닉네임
        Debug.Log("PlayerPrefs " + PlayerPrefs.GetString(playerNamePrefKey));//클라에 저장된 닉네임
    }

}
