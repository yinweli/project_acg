﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class Bullet_Revolver : MonoBehaviour 
{
    public AIBullet pAI = null;

    int iCount = 1;
    // ------------------------------------------------------------------
    void Start()
    {
        iCount = Rule.UpgradeWeaponRevolver();
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Tuple<int, bool> Damage;

            if (iCount < Rule.UpgradeWeaponRevolver())
                Damage = Rule.BulletDamage(pAI.iPlayer, false);
            else
                Damage = Rule.BulletDamage(pAI.iPlayer, true);

            if (other.gameObject.GetComponent<AIEnemy>())
                other.gameObject.GetComponent<AIEnemy>().AddHP(-Damage.Item1, Damage.Item2);

            if (Rule.GetWeaponLevel(ENUM_Weapon.Revolver) > 0)
                iCount--;

            if (iCount <= 0)
                Destroy(gameObject);

            pAI.Chace(GetTarget());
        }
    }
    // ------------------------------------------------------------------
    // 取得目標.
    GameObject GetTarget()
    {
        // 沒有可作為目標的怪物.
        if (SysMain.pthis.AtkEnemy.Count == 0)
            return null;

        GameObject ObjTarget = null;

        foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.AtkEnemy)
        {
            if (!ObjTarget)
                ObjTarget = itor.Key;

            float fDisTarget = Vector2.Distance(transform.position, ObjTarget.transform.position);
            float fDisObj = Vector2.Distance(transform.position, itor.Key.transform.position);

            // 比較距離.
            if (fDisTarget > fDisObj)
                ObjTarget = itor.Key;
        }

        return ObjTarget;
    }
    // ------------------------------------------------------------------

}