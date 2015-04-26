using UnityEngine;
using System.Collections;

public class P_Victory : MonoBehaviour 
{
    public GameObject[] ObjPage = new GameObject[3];
    public UILabel[] pLb = new UILabel[5];
	
    void Start()
    {
        GoogleAnalytics.pthis.LogScreen("Victory");
        GoogleAnalytics.pthis.LogEvent("Victory", "Day" + PlayerData.pthis.iStage, "Time: " + GameData.pthis.iStageTime + " Kill:" + GameData.pthis.iKill + " Live:" + PlayerData.pthis.Members.Count + " Dead:" + GameData.pthis.iDead, GameData.pthis.iStageTime);

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
