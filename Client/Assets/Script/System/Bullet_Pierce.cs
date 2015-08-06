using UnityEngine;
using System.Collections;

public class Bullet_Pierce : MonoBehaviour
{
    public AIBullet pAI = null;

    int iCount = 1;
    // ------------------------------------------------------------------
    void Start()
    {
        iCount = Rule.UpgradeWeaponRifle();
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (iCount < Rule.UpgradeWeaponRifle())
                pAI.bCriticalStrik = false;

            if (other.gameObject.GetComponent<AIEnemy>())
                other.gameObject.GetComponent<AIEnemy>().AddHP(-pAI.iDamage, pAI.bCriticalStrik);

            if (SysMain.pthis.iWLevel[(int)ENUM_Weapon.Rifle] > 0)
                iCount--;           

            if (iCount <= 0)
                Destroy(gameObject);
        }        
    }
    // ------------------------------------------------------------------
}
