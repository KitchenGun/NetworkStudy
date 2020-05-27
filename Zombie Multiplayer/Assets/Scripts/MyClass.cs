using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClass : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log(gameObject.name + " : "+MySingleton.Instance.MyTestString);
    }
}
