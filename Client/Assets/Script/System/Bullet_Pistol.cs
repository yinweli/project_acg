﻿using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class Bullet_Pistol : MonoBehaviour
{
    public AIBullet pAI = null;
    public Sprite pSprite = null;
    // ------------------------------------------------------------------
    void Start()
    {
        if (Rule.GetWeaponLevel(ENUM_Weapon.Pistol) > 0)
            pAI.pRander.sprite = pSprite;
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, true);

            if (other.gameObject.GetComponent<AIEnemy>())
			{
                other.gameObject.GetComponent<AIEnemy>().AddHP(-Damage.Item1, Damage.Item2);
				Statistics.pthis.RecordHit(ENUM_Damage.Pistol, Damage.Item1, true);
			}//if

            if(Rule.GetWeaponLevel(ENUM_Weapon.Pistol) > 0)
                other.gameObject.AddComponent<Freeze>().FreezeNow();

            Destroy(gameObject);
        }        
    }
    // ------------------------------------------------------------------
}
