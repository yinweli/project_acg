using UnityEngine;
using System.Collections;

public class G_Player : MonoBehaviour 
{
    public int iPlayer;

    public UI2DSprite[] Role = new UI2DSprite[(int)ENUM_Role.Role_Count];

    public AIPlayer pAI;
    // ------------------------------------------------------------------
    public void InitPlayer()
    {
        pAI = GetComponent<AIPlayer>();
        pAI.pWeapon = (WeaponType)PlayerData.pthis.Members[iPlayer].iEquip;

        // 實例化武器.
        if (pAI.pWeapon != WeaponType.Weapon_null)
        {
            GameObject pSWeapon = UITool.pthis.CreateUI(Role[(int)ENUM_Role.HandR].gameObject, "Prefab/" + pAI.pWeapon);
            UI2DSprite[] p2DS = pSWeapon.GetComponentsInChildren<UI2DSprite>();
            for (int i = 0; i < p2DS.Length; i++)
                p2DS[i].depth = p2DS[i].depth + (iPlayer * 15);

            // 拿手電筒需替玩家加上光源.
            if (pAI.pWeapon == WeaponType.Weapon_001)
            {
                GameObject pObj = UITool.pthis.CreateUI(gameObject, "Prefab/G_Light");
                pObj.GetComponent<G_Light>().SetLightFollow(pSWeapon);
                GetComponent<AIPlayer>().ObjTarget = pObj.GetComponent<G_Light>().pDrag.gameObject;
            }
            else
                pAI.pWAni = pSWeapon.GetComponent<Animator>();
        }

        // 依照角色切換layer.
        for (int i = 0; i < (int)ENUM_Role.Role_Count; i++)
            Role[i].depth = Role[i].depth + (iPlayer * 15);
    }
}
