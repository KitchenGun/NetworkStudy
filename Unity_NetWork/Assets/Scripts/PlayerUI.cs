using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Text playerNameText;
    [SerializeField]
    private Slider playerHealthSlider;

    private PlayerManager target;

    [SerializeField]
    private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

    float characterControllerHeight = 0f;

    Transform targetTransform;

    Vector3 targetPosition;

    public void SetTarget(PlayerManager _target)
    {
        if(_target==null)
        {
            Debug.LogError("_target is null");
        }
        target = _target;
        if(playerNameText!=null)
        {
            playerNameText.text = target.photonView.Owner.NickName;
        }
        CharacterController _characterController = _target.GetComponent<CharacterController>();
        if(_characterController!=null)
        {
            characterControllerHeight = _characterController.height;
        }
    }

    private void Awake()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    private void Update()
    {
        if(playerHealthSlider!= null)
        {
            playerHealthSlider.value = target.Health;
        }
        if(target==null)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    private void LateUpdate()
    {
        if(targetTransform!=null)
        {
            targetPosition = targetTransform.position;
            targetPosition.y += characterControllerHeight;
            this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
        }
    }
}
