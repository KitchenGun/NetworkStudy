using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyStringEvent : UnityEvent<string,int>//최대 4개 까지 받을수있다.
{

}

public class ExampleEvent : MonoBehaviour
{
    MyStringEvent m_MyEvent;
    //UnityEvent m_MyEvent;
    // Start is called before the first frame update
    private void Start()
    {
        if(m_MyEvent==null)
        {
            m_MyEvent = new MyStringEvent();
            m_MyEvent.AddListener(Ping);
            m_MyEvent.AddListener(Tag);
        }
    }
    private void Update()
    {
        if(Input.anyKeyDown&&m_MyEvent!=null)
        {
            m_MyEvent.Invoke("Test",1);
        }
    }

    private void Ping(string s,int a)
    {
        Debug.Log("Ping"+ s + a);
    }
    private void Tag(string s, int a)
    {
        Debug.Log("Tagging"+s+a);
    }
}
