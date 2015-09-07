﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolKit : MonoBehaviour 
{
    // 可抓人物佇列.
    static public Dictionary<GameObject, int> CatchRole = new Dictionary<GameObject, int>();
    // ------------------------------------------------------------------
	static public GameObject GetEnemyTarget()
    {
        if (CatchRole.Count <= 0)
            return null;

        int iTotal = 0;

        foreach (KeyValuePair<GameObject, int> itor in CatchRole)
            iTotal += itor.Value;

        int iPick = Random.Range(0, iTotal);

        foreach (KeyValuePair<GameObject, int> itor in CatchRole)
        {
            if (iPick < itor.Value)
                return itor.Key;

            iPick -= itor.Value;
        }
        return null;
    }
    // ------------------------------------------------------------------
    static public void ClearCatchRole()
    {
        foreach (KeyValuePair<GameObject, int> itor in CatchRole)
            Destroy(itor.Key);

        CatchRole.Clear();
    }
    // ------------------------------------------------------------------
    static public void SetLayer(int iLayer, SpriteRenderer[] pSprite)
    {
        // 切換layer.
        for (int i = 0; i < pSprite.Length; i++)
            pSprite[i].sortingOrder = pSprite[i].sortingOrder + (iLayer * 30);
    }
    // ------------------------------------------------------------------
    static public void MoveTo(GameObject pObj, Vector3 vecDirection, float fSpeed)
    {
        // 把z歸零, 因為沒有要動z值.
        vecDirection.z = 0;
        // 把物件位置朝目標向量(玩家方向)移動.
        pObj.transform.position += vecDirection.normalized * fSpeed * Time.deltaTime;
    }
    // ------------------------------------------------------------------
    static public UI2DSprite ChangeTo2DSprite(SpriteRenderer pRender)
    {
        pRender.gameObject.transform.localScale = Vector3.one;
        UI2DSprite pSprite = pRender.gameObject.AddComponent<UI2DSprite>();
        pSprite.sprite2D = pRender.sprite;
        pSprite.depth = pRender.sortingOrder;
        pSprite.color = pRender.color;
        pSprite.MakePixelPerfect();

        Destroy(pRender);

        return pSprite;
    }
    // ------------------------------------------------------------------
    static public GameObject AddWeaponTo2DSprite(GameObject pObj, ENUM_Weapon pWeapon)
    {
        GameObject ObjRHand = null;
        SpriteRenderer[] Role = pObj.GetComponentsInChildren<SpriteRenderer>();

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
