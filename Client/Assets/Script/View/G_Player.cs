using UnityEngine;
using System.Collections;

public class G_Player : MonoBehaviour 
{
    public int iPlayer;

    public UI2DSprite[] Role = new UI2DSprite[(int)ENUM_Role.Role_Count];

    public void InitPlayer()
    {
        WeaponType pType = (WeaponType)SysMain.pthis.Data.Data[iPlayer].iEquip;
        // 實例化武器.
        if (pType != WeaponType.Weapon_null)
        {
            GameObject pSWeapon = UITool.pthis.CreateUI(Role[(int)ENUM_Role.HandR].gameObject, "Prefab/" + pType);

            // 拿手電筒需替玩家加上光源.
            if (pType == WeaponType.Weapon_000)
            {
                GameObject pObj = UITool.pthis.CreateUI(gameObject, "Prefab/G_Light");
                pObj.GetComponent<G_Light>().SetLightFollow(pSWeapon);
            }
        }

        // 依照角色切換layer.
        for (int i = 0; i < (int)ENUM_Role.Role_Count; i++)
            Role[i].depth = Role[i].depth + (iPlayer * 15);
    }
}
