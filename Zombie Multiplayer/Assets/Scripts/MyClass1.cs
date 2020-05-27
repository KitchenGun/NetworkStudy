using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClass1 : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log(gameObject.name + " : "+MySingleton.Instance.MyTestString);
    }
}
