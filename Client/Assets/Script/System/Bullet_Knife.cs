using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class Bullet_Knife : MonoBehaviour
{
	public AIBullet pAI = null;
	// ------------------------------------------------------------------
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, true);
			
			if (other.gameObject.GetComponent<AIEnemy>())
			{
				other.gameObject.GetComponent<AIEnemy>().AddHP(-Damage.Item1, Damage.Item2);
				Statistics.pthis.RecordHit(ENUM_Damage.Knife, Damage.Item1, true);
			}//if

			Destroy(gameObject);
		}        
	}
	// ------------------------------------------------------------------
}
