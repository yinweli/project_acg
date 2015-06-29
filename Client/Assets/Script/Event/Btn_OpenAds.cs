using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Btn_OpenAds : MonoBehaviour 
{
    public UILabel pLb = null;
    public UISprite pIcon = null;
    public UISprite pMovie = null;

    string zoneID = "rewardedVideoZone";
    //string zoneID = "videoAds";    

    void Start()
    {
        pLb.text = "x" + Rule.AdsMoney();
    }

    void Update()
    {
        if (PlayerData.pthis.iAdsWatch >= PlayerData.pthis.iStage)
        {
            GetComponent<UIButton>().isEnabled = false;
            pLb.color = Color.gray;
            pIcon.color = Color.gray;
            pMovie.color = Color.gray;
        }
    }  

	void OnClick()
    {
        if (IsReady())
            ShowAd();
    }    
   
    private bool IsReady()
    {
        return UnityAdsHelper.IsReady(zoneID);
    }

    public void ShowAd()
    {
        UnityAdsHelper.ShowAd(zoneID, RewardUser);
		GoogleAnalytics.pthis.LogEvent("Count", "WatchAD", "", 0);
    }

    private void RewardUser()
    {
        // 增加獎勵.
        if (PlayerData.pthis.iStage - PlayerData.pthis.iAdsWatch > 0)
        {
            // 播給錢聲音.
            NGUITools.PlaySound(Resources.Load("Sound/FX/Buy") as AudioClip);
            // 給錢.
            Rule.CurrencyAdd(Rule.AdsMoney());
			// 已觀看次數增加.
			PlayerData.pthis.iAdsWatch++;
            // 存檔.
            SysMain.pthis.SaveGame();
			P_UI.pthis.UpdateCurrency();
			pLb.text = "x" + Rule.AdsMoney();
        }
    }    
}
