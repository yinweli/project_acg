using UnityEngine;
using System.Collections;

public class S_Weapon : MonoBehaviour
{
    public UILabel pLb_Src = null;
    public UISprite pS_Bg = null;
	
    public void SetLb(string pStr)
    {
        pLb_Src.text = pStr;
        pS_Bg.width = pLb_Src.width + 6;
    }
}
