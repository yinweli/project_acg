﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

			string szTemp = string.Format("{0}(I:{1},G:{2},U:{3})", 
			                              (ENUM_Resource)Itor, 
			                              ResourceStat.pthis.Init[Itor], 
			                              ResourceStat.pthis.Gain[Itor], 
			                              ResourceStat.pthis.Used[Itor]);

			GoogleAnalytics.pthis.LogEvent("Victory", "Day" + PlayerData.pthis.iStage, szTemp, 0);
			Debug.Log(szTemp);
		}//for

		Dictionary<int, int> PickupTotal = new Dictionary<int, int>();

		foreach(Pickup Itor in GameData.pthis.PickupList)
		{
			if(PickupTotal.ContainsKey(Itor.iType))
				PickupTotal[Itor.iType] += Itor.iCount;
			else
				PickupTotal[Itor.iType] = Itor.iCount;
		}//for

		foreach(KeyValuePair<int, int> Itor in PickupTotal)
		{
			string szTemp = string.Format("Pickup_{0}:{1}", (ENUM_Pickup)Itor.Key, Itor.Value);

			GoogleAnalytics.pthis.LogEvent("Victory", "Day" + PlayerData.pthis.iStage, szTemp, 0);
			Debug.Log(szTemp);
		}//for

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
