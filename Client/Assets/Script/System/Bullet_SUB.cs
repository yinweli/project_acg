using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class Bullet_SUB : MonoBehaviour
{
    public AIBullet pAI = null;

    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Tuple<int, bool> Damage = Rule.BulletDamage(pAI.iPlayer, false);

            if (Rule.GetWeaponLevel(ENUM_Weapon.SUB) > 0)
                GroundHurt(other.gameObject, Damage.Item1);
            else if(other.gameObject.GetComponent<AIEnemy>())
                other.gameObject.GetComponent<AIEnemy>().AddHP(-Damage.Item1, Damage.Item2);

            Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------------
    // 範圍傷害.
    void GroundHurt(GameObject pObj, int iDamage)
    {
        // 沒有可作為目標的怪物.
        if (!pObj || SysMain.pthis.AtkEnemy.Count <= 0)
            return;

        foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.Enemy)
        {
            float fDisObj = Vector2.Distance(pObj.transform.position, itor.Key.transform.position);

            // 比較距離.
            if (fDisObj < 0.5f && itor.Key && itor.Key.GetComponent<AIEnemy>() && itor.Key.GetComponent<AIEnemy>().iHP > 0)
                itor.Key.GetComponent<AIEnemy>().AddHP(-iDamage, false);            
        }
    }
    // ------------------------------------------------------------------
}
