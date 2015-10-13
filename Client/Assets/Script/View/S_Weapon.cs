using UnityEngine;
using System.Collections;

public class S_Weapon : MonoBehaviour
{
    public UILabel pLb_Src = null;
    public UISprite pS_Bg = null;
    // ------------------------------------------------------------------
    public void SetLb(string pStr)
    {
        pLb_Src.text = pStr;
        pS_Bg.width = pLb_Src.width + 6;
    }
    // ------------------------------------------------------------------
    public void SetLbActive(bool bIsActive)
    {
        pLb_Src.gameObject.SetActive(bIsActive);
        pS_Bg.gameObject.SetActive(bIsActive);
    }
}
