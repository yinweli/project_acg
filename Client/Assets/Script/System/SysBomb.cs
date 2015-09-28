using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SysBomb : MonoBehaviour 
{
    static public SysBomb pthis = null;

    void Awake()
    {
        pthis = this;
    }

    public void StartBomb()
    {
        SysUI.pthis.CreateUI(gameObject, "Prefab/G_SuperBomb");
		Statistics.pthis.RecordShot(ENUM_Damage.Bomb);
    }

    public void BombDamage()
    {
		int iDmage = 0;

		Rule.BombAdd(-1);

        foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.Enemy)
        {
            if (itor.Key && itor.Key.GetComponent<AIEnemy>())
			{
                itor.Key.GetComponent<AIEnemy>().AddHP(-GameDefine.iDamageBomb, false);
				iDmage += GameDefine.iDamageBomb;
			}//if
        }//for

		SysMain.pthis.SaveGame();
		Statistics.pthis.RecordHit(ENUM_Damage.Bomb, iDmage, iDmage > 0);
    }
}
