using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SysWalk : MonoBehaviour
{
    static public SysWalk pthis = null;

    public int iNextRoad = 1;

    public bool bTestMove = false;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    void Update()
    {
        if(!SysMain.pthis.bIsGaming || SysMain.pthis.Role.Count<=0)
            return;

        foreach (KeyValuePair<GameObject, int> itor in SysMain.pthis.Role)
            Move(itor.Key);
    }
    // ------------------------------------------------------------------
    void Move(GameObject pObj)
    {
        if (!pObj || !pObj.GetComponent<AIPlayer>())
            return;

        AIPlayer pAI = pObj.GetComponent<AIPlayer>();

        // 檢查是否可以移動
        if (!CheckMove(pAI))
            return;

        int[] iTargetPos = new int[SysMain.pthis.Role.Count];

        // 檢查前進目標.
        //if(pAI.iTeamPos) 

    }
    // ------------------------------------------------------------------
    bool CheckMove(AIPlayer pAI)  
    {
        if (!pAI)
            return false;

        if(pAI.bBeCaught || pAI.bBeTied)
            return false;

        return true;
    }
}
