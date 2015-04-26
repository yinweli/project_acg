using UnityEngine;
using System.Collections;

public class P_Login : MonoBehaviour 
{
    void Start()
    {
        //GoogleAnalyticsV3.instance.LogScreen("Login");
        // 選音樂.
        AudioCtrl.pthis.PlayMusic("Start");

        // 建立地圖物件.
        MapCreater.pthis.ShowMap(0);
        // 開始行走
        CameraCtrl.pthis.LoginMove();
    }
	// Update is called once per frame
    void Update()
    {
        if (GetComponent<UIPanel>().alpha < 0.002)
        {
            SysMain.pthis.CheckStart();
            Destroy(gameObject);
        }
    }
}
