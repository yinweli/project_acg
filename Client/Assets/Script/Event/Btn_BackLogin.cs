using UnityEngine;
using System.Collections;

public class Btn_BackLogin : MonoBehaviour {

	void OnClick()
    {
        // 隱藏戰鬥UI.
        P_UI.pthis.SetAlpha(0);
        // 建立登入.
        SysUI.pthis.CreatePanel("Prefab/P_Login");
        // 建立地圖.
        MapCreater.pthis.Create();
        // 開始行走
        CameraCtrl.pthis.LoginMove();

        Destroy(transform.parent.gameObject);
    }
}
