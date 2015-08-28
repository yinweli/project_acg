using UnityEngine;
using System.Collections;

public class G_Achievement : MonoBehaviour 
{
    public ENUM_Achievement pAchieve = ENUM_Achievement.Null;
	public int iLevel = 0;

    public UISprite pS_Icon;
    public UISprite pS_Check;

	public UILabel pLb_Name;
	public UILabel pLb_Progress;
	public UILabel pLb_Desc;
	public UILabel pLb_Effect;

	void Start()
	{
		Refresh();
	}
	public void Refresh()
	{
		DBFAchievement DBFTemp = (DBFAchievement)GameDBF.pthis.GetAchievement(pAchieve);

		if(DBFTemp == null)
			return;

		if(iLevel <= 0 || iLevel > GameDefine.iMaxAchievementLv)
			return;

		int iValueNow = DataAchievement.pthis.GetValue(pAchieve);
		int iValueNeed = DBFTemp.GetValue(iLevel);
		bool bComplete = iValueNow >= iValueNeed;

		pS_Check.enabled = bComplete;
		pLb_Name.text = GameDBF.pthis.GetLanguage(DBFTemp.Name) + " Lv " + iLevel;
		pLb_Progress.text = bComplete ? "---" : iValueNow + " / " + iValueNeed;
		pLb_Desc.text = GameDBF.pthis.GetLanguage(7000 + (int)pAchieve);

		DBFReward DBFTemp2 = (DBFReward)GameDBF.pthis.GetReward(DBFTemp.GetReward(iLevel));

		if(DBFTemp2 == null)
			return;

		if(DBFTemp2.Reward == (int)ENUM_Reward.Looks)
			pLb_Effect.text = GameDBF.pthis.GetLanguage(8000 + DBFTemp2.Reward);
		else
			pLb_Effect.text = string.Format(GameDBF.pthis.GetLanguage(8000 + DBFTemp2.Reward), DBFTemp2.Value);

	}
}
