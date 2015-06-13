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
    }

    private void RewardUser()
    {
        // 增加獎勵.
        if (PlayerData.pthis.iStage - PlayerData.pthis.iAdsWatch > 0)
        {
            // 已觀看次數增加.
            PlayerData.pthis.iAdsWatch++;
            // 播給錢聲音.
            NGUITools.PlaySound(Resources.Load("Sound/FX/Buy") as AudioClip);
            // 給錢.
            Rule.CurrencyAdd(Rule.AdsMoney());
            P_UI.pthis.UpdateCurrency();
            // 存檔.
            SysMain.pthis.SaveGame();
        }
        Debug.Log("Now Look: " + PlayerData.pthis.iAdsWatch);
    }    
}
