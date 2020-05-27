using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T :MonoBehaviour
{
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object(); //스레드관련
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if(m_ShuttingDown)//있으면 그냥 냅둠
            {
                Debug.LogWarning("[Singleton] instance'" + typeof(T) + "already destroy. Returning null.");
            }
            lock(m_Lock)
            {
                if(m_Instance==null)//없을 경우 생성
                {
                    var singletonObject = new GameObject();//오브젝트 생성
                    m_Instance = singletonObject.AddComponent<T>();//컴포넌트 생성
                    singletonObject.name = typeof(T).ToString() + "(Singleton)";

                    DontDestroyOnLoad(singletonObject);
                }
            }
            return m_Instance;
        }
    }

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }

    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }

}
