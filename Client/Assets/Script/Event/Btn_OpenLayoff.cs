using UnityEngine;
using System.Collections;

public class Btn_OpenLayoff : MonoBehaviour
{
    public G_ListRole pData = null;

    void OnClick()
    {
        GameObject pObj = SysUI.pthis.CreatePanel("Prefab/P_DelChr");
        pObj.GetComponent<P_DelChr>().pData = pData;      
    }
}
