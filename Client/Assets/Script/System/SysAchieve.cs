using UnityEngine;
using System.Collections;

public class SysAchieve : MonoBehaviour
{
    static public SysAchieve pthis = null;

    public int[] iTarget = new int[(int)ENUM_Achievement.Count];

    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
	// Use this for initialization
	void Start ()
    {
        for (int i = 1; i < (int)ENUM_Achievement.Count; i++)
        {
            DBFAchievement pDBF = (DBFAchievement)GameDBF.pthis.GetAchievement((ENUM_Achievement)i);

            for (int iLevel = 1; iLevel <= pDBF.MaxLevel; iLevel++)
                if (pDBF.GetValue(iLevel) > DataAchievement.pthis.GetValue((ENUM_Achievement)i))
                {
                    iTarget[i] = pDBF.GetValue(iLevel);
                    continue;
                }
        }
	}
    // ------------------------------------------------------------------
    public void UpdateReplace(ENUM_Achievement pAchieve, int iNewValue)
    {
        DBFAchievement pDBF = (DBFAchievement)GameDBF.pthis.GetAchievement(pAchieve);

        if (pDBF == null)
            return;

        if (iNewValue <= 0)
            return;

        Debug.Log("Update " + pAchieve + " Value: " + iNewValue);

        // 將數值先存檔.
        int iOldValue = DataAchievement.pthis.GetValue(pAchieve);
        int iMaxValue = pDBF.GetValue(pDBF.MaxLevel);
        iNewValue = iNewValue > iMaxValue ? iMaxValue : iNewValue;

        if (iNewValue > iOldValue)
        {
            DataAchievement.pthis.Data[(int)pAchieve] = iNewValue;
            DataAchievement.pthis.Save();

            CheckAchieve(pAchieve);
        }        
    }
    // ------------------------------------------------------------------
    public void UpdateTotal(ENUM_Achievement pAchieve, int iValue)
    {
        DBFAchievement pDBF = (DBFAchievement)GameDBF.pthis.GetAchievement(pAchieve);

        if (pDBF == null)
            return;

        if (iValue <= 0)
            return;

        Debug.Log("Update " + pAchieve + " Value: " + iValue);

        // 將數值先存檔.
        int iOldValue = DataAchievement.pthis.GetValue(pAchieve);
        int iNewValue = iOldValue + iValue;
        int iMaxValue = pDBF.GetValue(pDBF.MaxLevel);
        iNewValue = iNewValue > iMaxValue ? iMaxValue : iNewValue;
        DataAchievement.pthis.Data[(int)pAchieve] = iNewValue;
        DataAchievement.pthis.Save();

        CheckAchieve(pAchieve);
    }
    // ------------------------------------------------------------------
    public bool CheckAchieve(ENUM_Achievement pAchieve)
    {
        DBFAchievement pDBF = (DBFAchievement)GameDBF.pthis.GetAchievement(pAchieve);

        if (pDBF == null)
            return false;

        Debug.Log("CheckAchieve");

        // 檢查是否完成成就.
        if (DataAchievement.pthis.Data[(int)pAchieve] >= iTarget[(int)pAchieve])
        {
            Debug.Log("Finish " + pAchieve + " Value: " + DataAchievement.pthis.GetValue((ENUM_Achievement)pAchieve));

            ShowTip(pAchieve);

            for (int iLevel = 1; iLevel <= pDBF.MaxLevel; iLevel++)
                if (pDBF.GetValue(iLevel) > DataAchievement.pthis.GetValue((ENUM_Achievement)pAchieve))
                {
                    iTarget[(int)pAchieve] = pDBF.GetValue(iLevel);
                    continue;
                }
            
            return true;
        }

        return false;
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
