using UnityEngine;
using System.Collections;

public class G_Info : MonoBehaviour 
{
    public G_FeatureInfo[] pFInfo = new G_FeatureInfo[6];
    public UILabel[] Lb_Value = new UILabel[3];

    public void SetInfo(int iID)
    {
        for (int i = 0; i < pFInfo.Length; i++)
            pFInfo[i].SetInfo(i, iID);

        if ((ENUM_Weapon)PlayerData.pthis.Members[iID].iEquip == ENUM_Weapon.Weapon_001 || (ENUM_Weapon)PlayerData.pthis.Members[iID].iEquip == ENUM_Weapon.Weapon_null)
        {
            // 攻擊力.
            Lb_Value[0].text = "--";
            // 射擊速度.
            Lb_Value[1].text = "--";
            // 爆擊.
            Lb_Value[2].text = "--";
        }
        else
        {
            DBFEquip pDBFEquip = GameDBF.This.GetEquip(PlayerData.pthis.Members[iID].iEquip) as DBFEquip;
        
            // 攻擊力.
            Lb_Value[0].text = string.Format("{0}({1})",pDBFEquip.Damage, PlayerData.pthis.Members[iID].iAddDamage);
            // 射擊速度.
            Lb_Value[1].text = string.Format("1/{0}Sec", pDBFEquip.FireRate);
            // 爆擊.
            Lb_Value[2].text = string.Format("{0}({1})", pDBFEquip.CriticalStrike * 100, PlayerData.pthis.Members[iID].fCriticalStrike * 100);
        }        
    }
}
