using UnityEngine;
using System.Collections;

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
		Rule.BombaAdd(-1);

        foreach (GameObject ObjEnemy in SysMain.pthis.Enemy)
        {
            if (ObjEnemy && ObjEnemy.GetComponent<AIEnemy>())
                ObjEnemy.GetComponent<AIEnemy>().AddHP(-GameDefine.iDamageBomb, false);
        }
        SysMain.pthis.SaveGame();
    }
}
