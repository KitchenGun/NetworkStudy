using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySingleton : Singleton<MySingleton>
{
    protected MySingleton(){}

    public string MyTestString = "Hello Singleton";
}
