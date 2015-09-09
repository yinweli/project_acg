using UnityEngine;
using System.Collections;

public class EnemyLeoCat : MonoBehaviour 
{
    AIEnemy pAI = null;
    // ------------------------------------------------------------------
    void Start()
    {
        pAI = GetComponent<AIEnemy>();
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (!SysMain.pthis.bIsGaming)
            return;

        // 沒血逃跑.
        if (pAI.iHP <= 0)
        {
            Dead();
            return;
        }
    }
    // ------------------------------------------------------------------
    // 沒血死亡.
    void Dead()
    {
        // 播放死亡動作.
        pAI.AniPlay("Dead");
    }
    // ------------------------------------------------------------------
}
