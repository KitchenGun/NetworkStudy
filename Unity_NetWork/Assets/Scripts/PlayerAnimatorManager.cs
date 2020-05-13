using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerAnimatorManager : MonoBehaviourPun
{
    private Animator animator;

    [SerializeField]
    private float directionDampTime = 0.25f;//애니메이션 겹칠때 완충시켜주는 시간
    void Start()
    {
        animator = this.GetComponent<Animator>();
        if(!animator)
        {
            Debug.LogError("animator없음");
        }
    }

    void Update()
    {
        if(photonView.IsMine==false&&PhotonNetwork.IsConnected==true)
        {
            return;
        }
        if(!animator)
        {
            return;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //애니메이터의 레이어(0)의 상태를 확인하기 위하여
        if(stateInfo.IsName("Base Layer.Run"))
        {//달리는 애니메이션일때
            if(Input.GetButtonDown("Fire2"))
            {//입력받고
                animator.SetTrigger("Jump");
                //점프
            }
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        if(v<0)
        {
            v = 0;
        }
        
        animator.SetFloat("Speed", h * h + v * v);
        animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);

        //Debug.LogFormat("h v : {0} - {1}", h, v);입력확인용
    }
}
