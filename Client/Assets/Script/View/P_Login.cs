using UnityEngine;
using System.Collections;

public class P_Login : MonoBehaviour 
{
    void Start()
    {
        GoogleAnalyticsV3.getInstance().LogScreen("Login");
        // 選音樂.
        AudioCtrl.pthis.PlayMusic("Start", 0.9f);

        // 預先載入地圖物件.
        UITool.pthis.PreLoadMapObj(DataPlayer.pthis.iStyle);

        // 建立地圖物件.
        MapCreater.pthis.Show(0);
        // 開始行走
        CameraCtrl.pthis.LoginMove();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
