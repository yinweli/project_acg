using UnityEngine;
using System.Collections;

public class P_Copyright : MonoBehaviour 
{
    void Start()
    {
        GoogleAnalyticsV3.getInstance().LogScreen("Copyright");
        SysMain.pthis.ReadyStart();
    }
    public void DelCopyright()
    {
        // 建立遊戲開頭畫面.
        SysUI.pthis.CreatePanel("Prefab/P_Login");
        Destroy(gameObject);
    }
}
