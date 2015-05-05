using UnityEngine;
using System.Collections;

public class SysBomb : MonoBehaviour 
{
    static public SysBomb pthis = null;
    public Animator pAni;

    void Awake()
    {
        pthis = this;
    }

    public void StartBomb()
    {
        // 播放動畫.
        BombDamage();
    }

    public void BombDamage()
    {
        PlayerData.pthis.iBomb--;
        foreach (GameObject ObjEnemy in SysMain.pthis.Enemy)
        {
            if (ObjEnemy && ObjEnemy.GetComponent<AIEnemy>())
                ObjEnemy.GetComponent<AIEnemy>().AddHP(-GameDefine.iDamageBomb);
        }
        SysMain.pthis.SaveGame();
    }
}
