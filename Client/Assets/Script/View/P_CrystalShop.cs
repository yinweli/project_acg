using UnityEngine;
using System.Collections;

public class P_CrystalShop : MonoBehaviour
{
    public Btn_CrystalShop pMan;
    public UILabel pLbTime = null;

    public UISprite[] pS_Weapon = new UISprite[2];
    public UILabel[] pLb_Index = new UILabel[2];
    public UISprite[] pS_Box = new UISprite[2];
    // ------------------------------------------------------------------
	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < SysMain.pthis.pCollect.Length; i++)
        {
            pS_Weapon[i].spriteName = string.Format("ui_w{0:00}", (int)SysMain.pthis.pCollect[i].Weapon);
            pS_Weapon[i].MakePixelPerfect();
            pS_Weapon[i].gameObject.transform.localScale = ToolKit.GetWeaponIconScale(SysMain.pthis.pCollect[i].Weapon);
            pS_Box[i].color = SysMain.pthis.ColorLv[SysMain.pthis.pCollect[i].iLevel];
            pLb_Index[i].text = WeaponTransIndex(SysMain.pthis.pCollect[i].iIndex);
        }
	}
    // ------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
        if (!pMan)
        {
            Destroy(gameObject);
            return;
        }

        pLbTime.text = string.Format("{0:00}:{1:00}", pMan.iTimeCount / 60, pMan.iTimeCount % 60);
	}
    // ------------------------------------------------------------------
    public string WeaponTransIndex(int iIndex)
    {
        if (iIndex == 0)
            return "A";
        else if (iIndex == 1)
            return "B";
        else if (iIndex == 2)
            return "C";
        else if (iIndex == 3)
            return "D";
        else
            return "E";
    }
}
