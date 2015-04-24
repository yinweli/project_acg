using UnityEngine;
using System.Collections;

public class G_Info : MonoBehaviour 
{
    public Shader pShader = null;

    public GameObject S_Weapon = null;
    public GameObject S_Bullet = null;

    public G_FeatureInfo[] pFInfo = new G_FeatureInfo[6];
    public UILabel[] Lb_Value = new UILabel[3];

    GameObject ObjWeapon;
    GameObject ObjBullet;

    public void SetInfo(int iID)
    {
        for (int i = 0; i < pFInfo.Length; i++)
            pFInfo[i].SetInfo(i, iID);

        if ((ENUM_Weapon)PlayerData.pthis.Members[iID].iEquip != ENUM_Weapon.Weapon_null)
        {
            ObjWeapon = UITool.pthis.CreateUI(S_Weapon, "Prefab/" + (ENUM_Weapon)PlayerData.pthis.Members[iID].iEquip);
            UI2DSprite[] p2DS = ObjWeapon.GetComponentsInChildren<UI2DSprite>();

            for (int i = 0; i < p2DS.Length; i++)
            {
                p2DS[i].shader = pShader;
                p2DS[i].depth = p2DS[i].depth + 20;
            }

            DBFEquip pDBFEquip = GameDBF.This.GetEquip(PlayerData.pthis.Members[iID].iEquip) as DBFEquip;

            ObjBullet = UITool.pthis.CreateUI(S_Bullet, "Prefab/Item/G_" + (ENUM_Resource)pDBFEquip.Resource);
            ObjBullet.collider2D.enabled = false;
            p2DS = ObjBullet.GetComponentsInChildren<UI2DSprite>();

            for (int i = 0; i < p2DS.Length; i++)
            {
                p2DS[i].shader = pShader;
                p2DS[i].depth = p2DS[i].depth + 20;
            }
        }        

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

    public void Reset()
    {
        if (ObjWeapon)
            Destroy(ObjWeapon);

        if (ObjBullet)
            Destroy(ObjBullet);
    }
}
