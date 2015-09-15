using UnityEngine;
using System.Collections;

public class G_WeaponItem : MonoBehaviour 
{
    public int iMyIndex = 0;

    public UISprite pS_Weapon = null;
    public UILabel pLb_Index = null;
    public UISprite pS_Box = null;

    public void SetInfo(ENUM_Weapon pType, string sIndex)
    {
        pS_Weapon.spriteName = string.Format("ui_w{0:00}", (int)pType);
        pS_Weapon.MakePixelPerfect();
        pS_Weapon.gameObject.transform.localScale = ToolKit.GetWeaponIconScale(pType);

        pS_Box.color = SysMain.pthis.ColorLv[Rule.GetWeaponLevel(pType) + 1];
        pLb_Index.text = sIndex;
    }

    public void SetPos(bool bHaveOther)
    {
        if (bHaveOther && iMyIndex == 0)
            transform.localPosition = new Vector3(-90, 0, 0);
        else if (bHaveOther && iMyIndex == 1)
            transform.localPosition = new Vector3(90, 0, 0);
        else
            transform.localPosition = Vector3.zero;
    }
}
