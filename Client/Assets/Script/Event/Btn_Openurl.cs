﻿using UnityEngine;
using System.Collections;

public class Btn_Openurl : MonoBehaviour 
{
    public string targeturl = null;
    void OnClick()
    {
        string strurl = string.Format("window.open('{0}','_blank')", targeturl);

        Application.ExternalEval(strurl);
    }
}
