using UnityEngine;
using System.Collections;

public class Bullet_Freeze : MonoBehaviour
{
    public AIBullet pAI = null;
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<AIEnemy>())
                other.gameObject.GetComponent<AIEnemy>().AddHP(-pAI.iDamage, pAI.bCriticalStrik);

            if(SysMain.pthis.iWLevel[(int)ENUM_Weapon.Pistol] > 0)
                other.gameObject.AddComponent<Freeze>().FreezeNow();

            Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------------
}
