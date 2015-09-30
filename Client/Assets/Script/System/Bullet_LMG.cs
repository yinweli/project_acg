using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class Bullet_LMG : MonoBehaviour
{
    public AIBullet pAI = null;
	// ------------------------------------------------------------------
    void Start()
    {
		if(Rule.GetWeaponLevel(ENUM_Weapon.LMG) <= 0)
			return;

		Animator pAnimator = GetComponent<Animator>();

		if(pAnimator == null)
			return;

		GetComponent<Animator>().Play("Plasma");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Enemy")
			return;

		AIEnemy pEnemy = other.gameObject.GetComponent<AIEnemy>();

		if(pEnemy == null)
			return;

		Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, true);

		pEnemy.AddHP(-Damage.Item1 - Rule.GetWeaponLevel(ENUM_Weapon.LMG), Damage.Item2);
		Statistics.pthis.RecordHit(ENUM_Damage.LMG, Damage.Item1, true);

		Destroy(gameObject);
    }
    // ------------------------------------------------------------------
}
