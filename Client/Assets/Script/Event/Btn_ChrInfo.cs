using UnityEngine;
using System.Collections;

public class Btn_ChrInfo : MonoBehaviour
{
    public G_ListRole pData = null;
    // ------------------------------------------------------------------
    void OnPress(bool bIsDown)
    {
        if (bIsDown && pData.pInfo)
        {
            pData.pInfo.gameObject.SetActive(true);
            pData.pInfo.SetInfo(pData.iPlayerID);
        }
        else if (!bIsDown && pData.pInfo)
        {
            pData.pInfo.Reset();
            pData.pInfo.gameObject.SetActive(false);
        }
    }
}
