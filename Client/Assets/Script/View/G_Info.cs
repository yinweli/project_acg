using UnityEngine;
using System.Collections;

public class G_Info : MonoBehaviour 
{
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
            SpriteRenderer[] p2DS = ObjWeapon.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer pRender in p2DS)
                ToolKit.ChangeTo2DSprite(pRender);

            DBFEquip pDBFEquip = GameDBF.This.GetEquip(PlayerData.pthis.Members[iID].iEquip) as DBFEquip;

            ObjBullet = UITool.pthis.CreateUI(S_Bullet, "Prefab/Item/G_" + (ENUM_Resource)pDBFEquip.Resource);
            ObjBullet.GetComponent<Collider2D>().enabled = false;
            p2DS = ObjBullet.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer pRender in p2DS)
                ToolKit.ChangeTo2DSprite(pRender);
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
				int iUpgrade = PlayerData.pthis.Members[iID].iLiveStage * GameDefine.iDamageUpgrade + PlayerData.pthis.iDamageLv;
				string szResult = string.Format("[ffed00]{0}[-]", Mathf.Max(0, iEquip + iAddon + iUpgrade));
				string szExtra = "";
				
				if(iAddon != 0)
					szExtra += iAddon > 0 ? string.Format("[546ef2]+{0}[-]", iAddon) : string.Format("[e92121]{0}[-]", iAddon);

				if(iUpgrade != 0)
					szExtra += iUpgrade > 0 ? string.Format("[00ff00]+{0}[-]", iUpgrade) : string.Format("[e92121]{0}[-]", iUpgrade);

				if(szExtra.Length > 0)
					szResult += string.Format("({0}{1})", iEquip, szExtra);

				Lb_Value[0].text = szResult;
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
