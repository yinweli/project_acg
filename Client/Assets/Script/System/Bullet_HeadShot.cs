using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class Bullet_HeadShot : MonoBehaviour
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
                if (Damage.Item2)
                    pEnemy.HitSfx("G_HeadShot");
            }
            Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------------
}
