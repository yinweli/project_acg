using UnityEngine;
using System.Collections;

public class Btn_FireChr : MonoBehaviour
{
    public G_ListRole pData = null;

    void OnClick()
    {
        GameObject pObj = SysUI.pthis.CreatePanel("Prefab/P_DelChr");
        pObj.transform.localPosition = new Vector3(0, 0, -1000);        
    }
}
