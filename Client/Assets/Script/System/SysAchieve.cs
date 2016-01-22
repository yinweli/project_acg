using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SysAchieve : MonoBehaviour
{
    static public SysAchieve pthis = null;

    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    public void Add(ENUM_Achievement pAchieve, int iNewValue)
    {
		List<int> Complete = Rule.AchievementAdd(pAchieve, iNewValue);
		
		if(Complete.Count > 0)
			ShowTip(pAchieve);
    }
    // ------------------------------------------------------------------
    public void ShowTip(ENUM_Achievement pAchieve)
    {
        GameObject pObj = SysUI.pthis.CreatePanel("Prefab/P_AchieveTip");
        G_AchieveTip pScript = pObj.GetComponentInChildren<G_AchieveTip>();

        if (!pScript)
            return;

        pScript.pAchieve = pAchieve;
    }
}
