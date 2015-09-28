using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class Bullet_LMG : MonoBehaviour
{
    public AIBullet pAI = null;
    void Start()
    {
        if (Rule.GetWeaponLevel(ENUM_Weapon.LMG) > 0 && GetComponent<Animator>())
            GetComponent<Animator>().Play("Plasma");
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, true);

            if (other.gameObject.GetComponent<AIEnemy>())
			{
                other.gameObject.GetComponent<AIEnemy>().AddHP(-Damage.Item1 - Rule.GetWeaponLevel(ENUM_Weapon.LMG), Damage.Item2);
				Statistics.pthis.RecordHit(ENUM_Damage.LMG, Damage.Item1, true);
			}//if

            Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------------
}
