using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class P_Victory : MonoBehaviour 
{
    static public P_Victory pthis = null;

    public G_Feature pFeature = null;

    public GameObject[] ObjPage = new GameObject[3];
    public UILabel[] pLb = new UILabel[5];

    public GameObject ObjCrystalShop = null;

    public AudioClip Clip_Buy;
    public AudioClip Clip_CantBuy;
    // ------------------------------------------------------------------
    void Awake()
    {
        pthis = this;
    }
    // ------------------------------------------------------------------
    void Start()
    {
        GoogleAnalyticsV3.getInstance().LogScreen("Victory");

		GoogleAnalyticsV3.getInstance().LogEvent("PlayTime", "Day" + DataPlayer.pthis.iStage, "", DataGame.pthis.iStageTime);
		GoogleAnalyticsV3.getInstance().LogEvent("Victory", "Day" + DataPlayer.pthis.iStage, "", 1);

        // 天數.
        pLb[0].text = DataPlayer.pthis.iStage.ToString();
        // 關卡時間.
        pLb[1].text = string.Format("{0:00}:{1:00}:{2:00}",DataGame.pthis.iStageTime / 3600 ,(DataGame.pthis.iStageTime / 60) % 60, DataGame.pthis.iStageTime % 60);
        // 殺怪數.
        pLb[2].text = DataGame.pthis.iKill.ToString();
        // 殘餘人數.
		pLb[3].text = DataPlayer.pthis.MemberParty.Count.ToString();
        // 死亡人數.
        pLb[4].text = DataGame.pthis.iDead.ToString();

        AudioCtrl.pthis.PlayMusic("BG_Victory", 0.55f);
        NGUITools.PlaySound(Resources.Load("Sound/FX/Victory") as AudioClip);
    }
    // ------------------------------------------------------------------
    public void ChangePage(int iPage)
    {
        ObjPage[iPage].SetActive(false);
        ObjPage[iPage + 1].SetActive(true);

        if (ObjPage[iPage + 1] && ObjPage[iPage + 1].GetComponent<G_Feature>())
            ObjPage[iPage + 1].GetComponent<G_Feature>().OpenPage();

        if (iPage + 1 == 2)
        {
            GoogleAnalyticsV3.getInstance().LogScreen("Shop");
            ObjCrystalShop = SysUI.pthis.CreatePanel("Prefab/P_CrystalMan");
        }
    }
    // ------------------------------------------------------------------
    public void ClearPage()
    {
        AudioCtrl.pthis.pMusic.volume = 1f;

        if (ObjCrystalShop)
            Destroy(ObjCrystalShop);
    }
    // ------------------------------------------------------------------
}