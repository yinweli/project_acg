﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class Bullet_SUB : MonoBehaviour
{
    public AIBullet pAI = null;
    // ------------------------------------------------------------------
    void Start()
    {
        if (Rule.GetWeaponLevel(ENUM_Weapon.SUB) > 0 && GetComponent<Animator>())
            GetComponent<Animator>().Play("NewBullet");
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, true);
			AIEnemy pEnemy = other.gameObject.GetComponent<AIEnemy>();

			if (pEnemy)
			{
				pEnemy.AddHP(-Damage.Item1, Damage.Item2);
				Statistics.pthis.RecordDamage(ENUM_Damage.SUB, 1, Damage.Item1);
			}

            if (Rule.GetWeaponLevel(ENUM_Weapon.SUB) > 0)
                GroundHurt(other.gameObject.transform.position, Rule.UpgradeWeaponSUB());
            else
                Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------------
    // 範圍傷害.
    void GroundHurt(Vector3 vPos, int iDamage)
    {
        pAI.bCanMove = false;

        if(GetComponent<BoxCollider2D>())
            GetComponent<BoxCollider2D>().enabled = false;

        if (GetComponent<Animator>())
            GetComponent<Animator>().Play("Grenade");

        // 沒有可作為目標的怪物.
        if (SysMain.pthis.AtkEnemy.Count <= 0)
            return;
        
        pAI.pRander.material = Resources.Load("Sprite") as Material;
        gameObject.transform.localScale = new Vector3(GameDefine.fSUBArea / 0.1f, GameDefine.fSUBArea / 0.1f, 1);

        foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.Enemy)
        {
            float fDisObj = Vector2.Distance(vPos, itor.Key.transform.position);

            // 比較距離.
            if (fDisObj < GameDefine.fSUBArea && itor.Key && itor.Key.GetComponent<AIEnemy>() && itor.Key.GetComponent<AIEnemy>().iHP > 0)
            {
                itor.Key.GetComponent<AIEnemy>().AddHP(-iDamage, false);
                Statistics.pthis.RecordDamage(ENUM_Damage.SUB, 0, iDamage);
            }
        }        
    }
    // ------------------------------------------------------------------
    public void DelSelf()
    {
        Destroy(gameObject);
    }
}
