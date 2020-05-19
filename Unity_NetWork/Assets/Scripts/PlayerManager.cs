using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks,IPunObservable
{
    

    [SerializeField]
    private GameObject beams;

    bool IsFiring;

    public float Health = 1f;

    public static GameObject LocalPlayerInstance;

    [SerializeField]
    private GameObject playerUIPrefab;


    private void Awake()
    {
        if (beams == null)
        {
            Debug.LogError("<Color=red><a>Missing</a></Color>Beams REF", this);
        }
        else
        {
            beams.SetActive(false);
        }

        if(photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
      
        CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
        if(_cameraWork!=null)
        {
            if(photonView.IsMine)//자기 자신의 캐릭터를 제어할때 사용하는 권한 확인용
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("CameraWork컴포넌트에러");
        }

        UnityEngine.SceneManagement.SceneManager.sceneLoaded+=(scene,loadingMode)=>
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        };
        SetupUI();
    }
    //void OnLevelWasLoaded(int level)
    //{
    //    this.CalledOnLevelWasLoaded(level);
    //}
    private void CalledOnLevelWasLoaded(int level)
    {//레벨이 바뀔때 실행
        Debug.Log("level was loaded");
        
        try
        {
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
         
        }
    }

    private void SetupUI()
    {
        if (playerUIPrefab != null && photonView.IsMine)
        {//ui가 없을 경우
            //playerUIPrefab = Resources.Load<GameObject>("PlayerUI");
            GameObject _uiGo = playerUIPrefab;
            _uiGo.GetComponent<PlayerUI>();
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            //this.playerUIPrefab.GetComponent<PlayerUI>();
            //this.playerUIPrefab.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("player ui Prefab.");
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            ProcessInputs();
        }
        if(Health<=0)
        {
            GameManager.Instance.LeaveRoom();
        }

        if (beams != null && IsFiring != beams.activeInHierarchy)
        {
            beams.SetActive(IsFiring);
        }
    }

    private void ProcessInputs()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(!IsFiring)
            {
                IsFiring = true;
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (IsFiring)
            {
                IsFiring = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!photonView.IsMine)
        {
            return;
        }
        if(!other.name.Contains("Beam"))
        {//빔이 아니면 리턴
            return;
        }
        Health -= 0.1f;
    }

    private void OnTriggerStay(Collider other)
    {
        if(!photonView.IsMine)//클라이언트에서 진행중이면 true
        {
            return;
        }
        if (!other.name.Contains("Beam"))
        {
            return;
        }
        Health -= 0.1f*Time.deltaTime;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {//체력과 공격중 동기화
        if(stream.IsWriting)
        {
            stream.SendNext(IsFiring);
            stream.SendNext(Health);
        }
        else
        {
            this.IsFiring = (bool)stream.ReceiveNext();
            this.Health = (float)stream.ReceiveNext();
        }
    }
}
