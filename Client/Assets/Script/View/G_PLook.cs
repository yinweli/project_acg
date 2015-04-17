using UnityEngine;
using System.Collections;

public class G_PLook : MonoBehaviour 
{
    public UI2DSprite[] Role;
    public void SetLook(AIPlayer pAI, int iLayer, WeaponType pWeapon)
    {
        GameObject ObjRHand = null;
        Role = GetComponentsInChildren<UI2DSprite>();

        // 取得右手.
        for (int i = 0; i < Role.Length; i++)
            if (Role[i].gameObject.name == "S_Hand_R")
                ObjRHand = Role[i].gameObject.transform.parent.gameObject;

        pAI.pAni = GetComponent<Animator>();

        // 實例化武器.
        if (pWeapon != WeaponType.Weapon_null)
        {
            GameObject ObjWeapon = UITool.pthis.CreateUI(ObjRHand, "Prefab/" + pAI.pWeapon);
            UI2DSprite[] p2DS = ObjWeapon.GetComponentsInChildren<UI2DSprite>();
            // 修改武器layer
            for (int i = 0; i < p2DS.Length; i++)
            {
                p2DS[i].depth = p2DS[i].depth + (iLayer * 20);

                Vector3 vecPos = p2DS[i].gameObject.transform.localPosition;
                vecPos.z = -0.00002f * (float)p2DS[i].depth;
                p2DS[i].gameObject.transform.localPosition = vecPos;
            }

            // 拿手電筒需替玩家加上光源.
            if (pWeapon == WeaponType.Weapon_001)
            {
                GameObject pObj = UITool.pthis.CreateUI(pAI.gameObject, "Prefab/G_Light");
                pObj.GetComponent<G_Light>().SetLightFollow(ObjWeapon);
                pAI.ObjTarget = pObj.GetComponent<G_Light>().pDrag.gameObject;
            }
            else
                pAI.pWAni = ObjWeapon.GetComponent<Animator>();
        }

        // 依照角色切換layer.
        for (int i = 0; i < Role.Length; i++)
        {
            Role[i].depth = Role[i].depth + (iLayer * 20);

            Vector3 vecPos = Role[i].gameObject.transform.localPosition;
            vecPos.z = -0.00002f * (float)Role[i].depth;
            Role[i].gameObject.transform.localPosition = vecPos;
        }

        //Destroy(this);
    }
}
