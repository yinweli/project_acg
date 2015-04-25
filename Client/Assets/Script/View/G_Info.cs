﻿using UnityEngine;
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
            ObjWeapon.transform.localPosition = new Vector3(-20,0,0);
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

			// 攻擊力
			{
				int iEquip = pDBFEquip.Damage;
				int iAddon = PlayerData.pthis.Members[iID].iAddDamage;
				string szTemp = string.Format("[ffed00]{0}[-]", Mathf.Max(0, iEquip + iAddon));
				
				if(iAddon != 0)
				{
					if(iAddon > 0)
						szTemp += string.Format("({0}[546ef2]+{1}[-])", iEquip, iAddon);
					else
						szTemp += string.Format("({0}[e92121]{1}[-])", iEquip, iAddon);
				}//if

				Lb_Value[0].text = szTemp;
			}

            // 射擊速度
			Lb_Value[1].text = string.Format("[ffed00]{0:0.0}[-] per sec", 1 / pDBFEquip.FireRate);

            // 爆擊
			{
				int iEquip = (int)(pDBFEquip.CriticalStrike * 100);
				int iAddon = (int)(PlayerData.pthis.Members[iID].fCriticalStrike * 100);
				string szTemp = string.Format("[ffed00]{0}[-]", Mathf.Max(0, iEquip + iAddon));

				if(iAddon != 0)
				{
					if(iAddon > 0)
						szTemp += string.Format("({0}[546ef2]+{1}[-])", iEquip, iAddon);
					else
						szTemp += string.Format("({0}[e92121]{1}[-])", iEquip, iAddon);
				}//if

				Lb_Value[2].text = szTemp + "%";
			}
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