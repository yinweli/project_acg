using UnityEngine;
using System.Collections;

public class G_AchieveTip : MonoBehaviour
{
    public ENUM_Achievement pAchieve = ENUM_Achievement.Null;
    public int iLevel = 0;

    public UILabel pLb_Name = null;
    public UILabel pLb_Info = null;

	// Use this for initialization
	void Start ()
    {
        DBFAchievement DBFTemp = (DBFAchievement)GameDBF.pthis.GetAchievement(pAchieve);

        pLb_Name.text = GameDBF.pthis.GetLanguage(DBFTemp.Name) + " Lv " + iLevel;
        pLb_Info.text = GameDBF.pthis.GetLanguage(8000 + (int)pAchieve);
	}
}
