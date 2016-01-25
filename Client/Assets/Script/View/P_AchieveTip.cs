using UnityEngine;
using System.Collections;

public class P_AchieveTip : MonoBehaviour
{
    public ENUM_Achievement pAchieve = ENUM_Achievement.Null;

    public UILabel pLb_Name = null;
    public UILabel pLb_Info = null;

    public GameObject G_Icon = null;
    public UISprite pS_Icon = null;

	// Use this for initialization
	void Start ()
    {
        DBFAchievement DBFTemp = (DBFAchievement)GameDBF.pthis.GetAchievement(pAchieve);

        pLb_Name.text = GameDBF.pthis.GetLanguage(DBFTemp.Name);
        pLb_Info.text = GameDBF.pthis.GetLanguage(8000 + (int)pAchieve);
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
