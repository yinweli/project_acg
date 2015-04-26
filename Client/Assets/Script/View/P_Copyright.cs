using UnityEngine;
using System.Collections;

public class P_Copyright : MonoBehaviour 
{
    void Start()
    {
        SysMain.pthis.ReadyStart();
    }
    public void DelSelf()
    {
        // 建立遊戲開頭畫面.
        SysUI.pthis.CreatePanel("Prefab/P_Login");

        Destroy(gameObject);
    }
}
