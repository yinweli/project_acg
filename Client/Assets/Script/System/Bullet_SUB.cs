using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class Bullet_SUB : MonoBehaviour
{
    public AIBullet pAI = null;
    // ------------------------------------------------------------------
    void Start()
    {
		if(Rule.GetWeaponLevel(ENUM_Weapon.SUB) <= 0)
			return;

		Animator pAnimator = GetComponent<Animator>();

		if(pAnimator == null)
			return;

		GetComponent<Animator>().Play("NewBullet");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Enemy")
			return;

		AIEnemy pEnemy = other.gameObject.GetComponent<AIEnemy>();
		
		if(pEnemy == null)
			return;

		Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, true);

		pEnemy.AddHP(-Damage.Item1, Damage.Item2);
		Statistics.pthis.RecordHit(ENUM_Damage.SUB, Damage.Item1, true);

		if (Rule.GetWeaponLevel(ENUM_Weapon.SUB) > 0)
			GroundHurt(other.gameObject.transform.position, Rule.UpgradeWeaponSUB());
		else
			Destroy(gameObject);
    }
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
        
        pAI.pRander.material = UITool.pthis.M_Sprite;
        gameObject.transform.localScale = new Vector3(GameDefine.fSUBArea / 0.1f, GameDefine.fSUBArea / 0.1f, 1);

        foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.Enemy)
        {
			if(itor.Key == null)
				continue;

			AIEnemy pEnemy = itor.Key.GetComponent<AIEnemy>();

			if(pEnemy == null)
				continue;

			if(pEnemy.iHP <= 0)
				continue;

            float fDisObj = Vector2.Distance(vPos, itor.Key.transform.position);

			// 比較距離.
			if(fDisObj >= GameDefine.fSUBArea)
				continue;

			pEnemy.AddHP(-iDamage, false);
			Statistics.pthis.RecordHit(ENUM_Damage.SUB, iDamage, false);
        }        
    }
    public void DelSelf()
    {
        Destroy(gameObject);
    }
	// ------------------------------------------------------------------
}
