using UnityEngine;
using System.Collections;

public class G_Achievement : MonoBehaviour 
{
    public ENUM_Achievement pAchieve = ENUM_Achievement.Null;
	public int iLevel = 0;

    public GameObject G_Icon;
    public UISprite pS_Icon;
    public UISprite pS_Check;

	public UILabel pLb_Name;
	public UILabel pLb_Progress;
	public UILabel pLb_Desc;
	public UILabel pLb_Effect;
    // ------------------------------------------------------------------
	void Start()
	{
		Refresh();
	}
    // ------------------------------------------------------------------
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
		pLb_Desc.text = GameDBF.pthis.GetLanguage(8000 + (int)pAchieve);

		DBFReward DBFTemp2 = (DBFReward)GameDBF.pthis.GetReward(DBFTemp.GetReward(iLevel));

		if(DBFTemp2 == null)
			return;

        if (DBFTemp2.Reward == (int)ENUM_Reward.Looks)
            pLb_Effect.text = GameDBF.pthis.GetLanguage(9000 + DBFTemp2.Note);
        else
			pLb_Effect.text = string.Format(GameDBF.pthis.GetLanguage(9000 + DBFTemp2.Note), DBFTemp2.Value);

        RefreshIcon(DBFTemp2);
	}
    // ------------------------------------------------------------------
    void RefreshIcon(DBFReward pReward)
    {
        pS_Icon.gameObject.SetActive(true);

        if (pReward.Reward == (int)ENUM_Reward.Looks)
        {
            pS_Icon.gameObject.SetActive(false);

            GameObject ObjHuman = UITool.pthis.CreateRole(G_Icon, pReward.Value);
            ToolKit.AddWeaponTo2DSprite(ObjHuman, ENUM_Weapon.Null, 15);

            ObjHuman.transform.localPosition = new Vector3(0, 90, 0);
        }
        else if (pReward.Reward == (int)ENUM_Reward.Currency)
            pS_Icon.spriteName = "ui_027";
        else if (pReward.Reward == (int)ENUM_Reward.Battery)
            pS_Icon.spriteName = "ui_com_001_full";
        else if (pReward.Reward == (int)ENUM_Reward.LightAmmo)
            pS_Icon.spriteName = "ui_wpn_001";
        else if (pReward.Reward == (int)ENUM_Reward.HeavyAmmo)
            pS_Icon.spriteName = "ui_wpn_002";
        else if (pReward.Reward == (int)ENUM_Reward.Bomb)
            pS_Icon.spriteName = "ui_013";

        pS_Icon.MakePixelPerfect();
    }
}
