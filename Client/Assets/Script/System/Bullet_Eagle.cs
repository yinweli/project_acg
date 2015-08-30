using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class Bullet_Eagle : MonoBehaviour
{
    public AIBullet pAI = null;
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            AIEnemy pEnemy = other.gameObject.GetComponent<AIEnemy>();
            if (pEnemy)
            {
                Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, true);

                pEnemy.AddHP(-Damage.Item1, false);
				Statistics.pthis.RecordDamage(ENUM_Damage.Eagle, 1, Damage.Item1);

                if (Damage.Item2 && Rule.GetWeaponLevel(ENUM_Weapon.Eagle) > 0)
                    pEnemy.HitSfx("G_HeadShot");
                else if(Damage.Item2)
                    pEnemy.HitSfx("G_Crit");
            }
            Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------------
}
