using UnityEngine;
using LibCSNStandard;
using System.Collections;
using System.Collections.Generic;

public class P_Victory : MonoBehaviour 
{
    public GameObject[] ObjPage = new GameObject[3];
    public UILabel[] pLb = new UILabel[5];
	
    void Start()
    {
        SysUI.pthis.pVictory = this;

		GoogleAnalytics.pthis.LogEvent("PlayTime", "Day" + DataPlayer.pthis.iStage, "", DataGame.pthis.iStageTime);
		GoogleAnalytics.pthis.LogEvent("Victory", "Day" + DataPlayer.pthis.iStage, "", 1);

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
