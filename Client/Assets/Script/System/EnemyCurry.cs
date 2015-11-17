using UnityEngine;
using System.Collections;

// 慶祝2015勇士兩勝的戰將Curry!特把Carry改為Curry.
public class EnemyCurry : MonoBehaviour
{
    AIEnemy pAI = null;

    public GameObject ObjTarget = null;

    public int iReleaseHp = 0;
    // ------------------------------------------------------------------
    void Start()
    {
        pAI = GetComponent<AIEnemy>();
        // 播放抓人動作.
        pAI.AniPlay("Catch");
        
        if (pAI.iMonster == 1001 || pAI.iMonster == 1004 || pAI.iMonster == 1007)
        {
            EnemyCurry[] temp = GetComponents<EnemyCurry>();
            iReleaseHp = pAI.iHP - (pAI.iHP / 10 * temp.Length);
        }
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if (pAI.iHP <= iReleaseHp)
            ReleaseWarriors();
    }
    // ------------------------------------------------------------------
    // 拯救勇士!
    public void ReleaseWarriors()
    {
        if (ObjTarget && ObjTarget.GetComponent<AIPlayer>())
            ObjTarget.GetComponent<AIPlayer>().BeFree();
        Destroy(this);
    }
    // ------------------------------------------------------------------
    // 我姆斯啦!
    public void KillJames()
    {
        if (ObjTarget && ObjTarget.GetComponent<AIPlayer>())
            ObjTarget.GetComponent<AIPlayer>().BeKill();
        Destroy(this);    
    }
    // ------------------------------------------------------------------
}
