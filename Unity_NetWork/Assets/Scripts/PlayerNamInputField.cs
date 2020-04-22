using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;


[RequireComponent(typeof(InputField))]
public class PlayerNamInputField : MonoBehaviour
{
    const string playerNamePrefKey = "PlatyerName";
    private InputField _inputField;

    // Start is called before the first frame update
    private void Start()
    {
        string defaultName = string.Empty;
        _inputField = this.GetComponent<InputField>();
        if((_inputField!=null))
        {
            if(PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }
        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName()
    {
        string value = _inputField.text;
        if(string.IsNullOrEmpty(value))
        {
            Debug.LogError("name is empty");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }

    private void OnDestroy()
    {
        Debug.Log("PhotonNetwork.Nickname " + PhotonNetwork.NickName);
        Debug.Log("PlayerPrefs " + PlayerPrefs.GetString(playerNamePrefKey));
    }

}
