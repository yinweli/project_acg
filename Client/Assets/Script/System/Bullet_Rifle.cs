using UnityEngine;
using LibCSNStandard;
using System.Collections;

public class Bullet_Rifle : MonoBehaviour
{
    public AIBullet pAI = null;
    public Sprite pSprite = null;
    int iCount = 1;
    // ------------------------------------------------------------------
    void Start()
    {
        iCount = Rule.UpgradeWeaponRifle();
        if (Rule.GetWeaponLevel(ENUM_Weapon.Rifle) > 0)
            pAI.pRander.sprite = pSprite;
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Tuple<int, bool> Damage;

            if (iCount < Rule.UpgradeWeaponRifle())
                Damage = Rule.BulletDamage(pAI.iPlayer, false);
            else
                Damage = Rule.BulletDamage(pAI.iPlayer, true);

            if (other.gameObject.GetComponent<AIEnemy>())
                other.gameObject.GetComponent<AIEnemy>().AddHP(-Damage.Item1, Damage.Item2);

            if (Rule.GetWeaponLevel(ENUM_Weapon.Rifle) > 0)
                iCount--;           

            if (iCount <= 0)
                Destroy(gameObject);
        }        
    }
    // ------------------------------------------------------------------
}
