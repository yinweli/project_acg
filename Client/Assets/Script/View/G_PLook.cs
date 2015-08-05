using UnityEngine;
using System.Collections;

public class G_PLook : MonoBehaviour 
{
    public void SetLook(AIPlayer pAI, int iLayer, ENUM_Weapon pWeapon)
    {
        GameObject ObjRHand = null;
        SpriteRenderer[] Role = GetComponentsInChildren<SpriteRenderer>();

        // 取得右手.
        for (int i = 0; i < Role.Length; i++)
            if (Role[i].gameObject.name == "S_Hand_R")
                ObjRHand = Role[i].gameObject.transform.parent.gameObject;

        // 實例化武器.
        if (pWeapon != ENUM_Weapon.Null)
        {
            GameObject ObjWeapon = UITool.pthis.CreateUI(ObjRHand, "Prefab/Weapon/" + pAI.pWeapon);

            // 修改武器layer.
            ToolKit.SetLayer(iLayer, ObjWeapon.GetComponentsInChildren<SpriteRenderer>());

            // 拿手電筒需替玩家加上光源.
            if (pWeapon == ENUM_Weapon.Light)
            {
                GameObject pObj = UITool.pthis.CreateUI(pAI.gameObject, "Prefab/G_Light");
                pObj.GetComponent<G_Light>().SetLightFollow(ObjWeapon);
                pAI.ObjTarget = pObj.GetComponent<G_Light>().pDrag.gameObject;
            }
            else
                pAI.pAction.pWAni = ObjWeapon.GetComponent<Animator>();
        }

        // 依照角色切換layer.
        ToolKit.SetLayer(iLayer, Role);
        Destroy(this);
    }

    public GameObject ChangeTo2DSprite(ENUM_Weapon pWeapon)
    {
        GameObject ObjRHand = null;
        SpriteRenderer[] Role = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer pRender in Role)
        {
            UI2DSprite pSprite = ToolKit.ChangeTo2DSprite(pRender);

            // 取得右手.
            if (pSprite.gameObject.name == "S_Hand_R")
                ObjRHand = pSprite.gameObject.transform.parent.gameObject;

            if (pSprite.gameObject.name == "Eye_bg_L" || pSprite.gameObject.name == "Eye_bg_R")
            {
                pSprite.width = 12;
                pSprite.height = 12;
            }            
        }

        if (pWeapon != ENUM_Weapon.Null)
        {
            GameObject ObjWeapon = UITool.pthis.CreateUI(ObjRHand, "Prefab/Weapon/" + pWeapon);
            SpriteRenderer[] p2DS = ObjWeapon.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer pRender in p2DS)
                ToolKit.ChangeTo2DSprite(pRender);
        }

        return ObjRHand;
    }
}
