using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class Bullet_Eagle : MonoBehaviour
{
    public AIBullet pAI = null;
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
		if(other.gameObject.tag != "Enemy")
			return;

		AIEnemy pEnemy = other.gameObject.GetComponent<AIEnemy>();

		if(pEnemy == null)
			return;

		Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, true);

		pEnemy.AddHP(-Damage.Item1, false);
		Statistics.pthis.RecordHit(ENUM_Damage.Eagle, Damage.Item1, true);

		// 播放爆擊特效
		if(Damage.Item2)
		{
			if(Rule.GetWeaponLevel(ENUM_Weapon.Eagle) > 0)
				pEnemy.HitSfx("G_HeadShot");
			else
				pEnemy.HitSfx("G_Crit");
		}//if

		Destroy(gameObject);
    }
    // ------------------------------------------------------------------
}
