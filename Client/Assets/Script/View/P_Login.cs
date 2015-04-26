﻿using UnityEngine;
using System.Collections;

public class P_Login : MonoBehaviour 
{
    void Start()
    {
        GoogleAnalytics.pthis.LogScreen("Login");
        // 選音樂.
        AudioCtrl.pthis.PlayMusic("Start");

        // 建立地圖物件.
        MapCreater.pthis.ShowMap(0);
        // 開始行走
        CameraCtrl.pthis.LoginMove();
    }

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
