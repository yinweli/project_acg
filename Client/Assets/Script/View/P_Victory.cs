﻿using UnityEngine;
using System.Collections;

public class P_Victory : MonoBehaviour 
{
    public GameObject[] ObjPage = new GameObject[3];
    public UILabel[] pLb = new UILabel[5];
	
    void Start()
    {
        GoogleAnalytics.pthis.LogScreen("Victory");
        GoogleAnalytics.pthis.LogEvent("Victory", "Day" + PlayerData.pthis.iStage, "Time:" + GameData.pthis.iStageTime, GameData.pthis.iStageTime);
        GoogleAnalytics.pthis.LogEvent("Victory", "Day" + PlayerData.pthis.iStage, "Kill:" + GameData.pthis.iKill, GameData.pthis.iKill);
        GoogleAnalytics.pthis.LogEvent("Victory", "Day" + PlayerData.pthis.iStage, "Live:" + PlayerData.pthis.Members.Count, PlayerData.pthis.Members.Count);
        GoogleAnalytics.pthis.LogEvent("Victory", "Day" + PlayerData.pthis.iStage, "Dead:" + GameData.pthis.iDead, GameData.pthis.iDead);

		foreach(int Itor in System.Enum.GetValues(typeof(ENUM_Resource)))
		{
			if(Itor == (int)ENUM_Resource.Null)
				continue;

			if(Itor == (int)ENUM_Resource.Resource_Count)
				continue;

			GoogleAnalytics.pthis.LogEvent("Victory", "Day" + PlayerData.pthis.iStage, string.Format("{0}_Init:{1}", (ENUM_Resource)Itor, ResourceStat.pthis.Init[Itor]), ResourceStat.pthis.Init[Itor]);
			GoogleAnalytics.pthis.LogEvent("Victory", "Day" + PlayerData.pthis.iStage, string.Format("{0}_Gain:{1}", (ENUM_Resource)Itor, ResourceStat.pthis.Gain[Itor]), ResourceStat.pthis.Gain[Itor]);
			GoogleAnalytics.pthis.LogEvent("Victory", "Day" + PlayerData.pthis.iStage, string.Format("{0}_Used:{1}", (ENUM_Resource)Itor, ResourceStat.pthis.Used[Itor]), ResourceStat.pthis.Used[Itor]);
		}//for

		ResourceStat.pthis.Report();

        // 天數.
        pLb[0].text = PlayerData.pthis.iStage.ToString();
        // 關卡時間.
        pLb[1].text = string.Format("{0:00}:{1:00}:{2:00}",GameData.pthis.iStageTime / 3600 ,(GameData.pthis.iStageTime / 60) % 60, GameData.pthis.iStageTime % 60);
        // 殺怪數.
        pLb[2].text = GameData.pthis.iKill.ToString();
        // 殘餘人數.
        pLb[3].text = PlayerData.pthis.Members.Count.ToString();
        // 死亡人數.
        pLb[4].text = GameData.pthis.iDead.ToString();

        AudioCtrl.pthis.pMusic.volume = 0.5f;
        NGUITools.PlaySound(Resources.Load("Sound/FX/Victory") as AudioClip);
    }

    public void OnDestory()
    {
        AudioCtrl.pthis.pMusic.volume = 1f;
    }

    public void ChangePage(int iPage)
    {
        ObjPage[iPage].SetActive(false);
        ObjPage[iPage + 1].SetActive(true);

        if (ObjPage[iPage + 1] && ObjPage[iPage + 1].GetComponent<G_Feature>())
            ObjPage[iPage + 1].GetComponent<G_Feature>().OpenPage();
    }
}
