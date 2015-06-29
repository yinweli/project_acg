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
        SysUI.pthis.CreateUI(gameObject, "Prefab/G_bomb");
    }

    public void BombDamage()
    {
		Rule.BombAdd(-1);

        foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.Enemy)
        {
            if (itor.Key && itor.Key.GetComponent<AIEnemy>())
                itor.Key.GetComponent<AIEnemy>().AddHP(-GameDefine.iDamageBomb, false);
        }
        SysMain.pthis.SaveGame();
    }
}
