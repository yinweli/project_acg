using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class Bullet_Knife : MonoBehaviour
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

		pEnemy.AddHP(-Damage.Item1, Damage.Item2);
		Statistics.pthis.RecordHit(ENUM_Damage.Knife, Damage.Item1, true);

		Destroy(gameObject);
	}
	// ------------------------------------------------------------------
}
