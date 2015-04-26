using UnityEngine;
using System.Collections;

public class P_Failed : MonoBehaviour 
{
    public UISprite pTitleBg = null;
    public GameObject ObjRecord = null;
    public UILabel[] pLb = new UILabel[3];

    void Start()
    {
        GoogleAnalytics.pthis.LogScreen("Failed");
        GoogleAnalytics.pthis.LogEvent("Failed", "Day" + PlayerData.pthis.iStage, "Time:" + PlayerData.pthis.iPlayTime, PlayerData.pthis.iPlayTime);
        GoogleAnalytics.pthis.LogEvent("Failed", "Day" + PlayerData.pthis.iStage, "Kill:" + PlayerData.pthis.iEnemyKill, PlayerData.pthis.iEnemyKill);
        GoogleAnalytics.pthis.LogEvent("Failed", "Day" + PlayerData.pthis.iStage, "Loss:" + PlayerData.pthis.iPlayerLost, PlayerData.pthis.iPlayerLost);

        StartCoroutine(OpenPage());
        // 天數.
        pLb[0].text = PlayerData.pthis.iStage.ToString();
        // 關卡時間.
        pLb[1].text = string.Format("{0:00}:{1:00}:{2:00}", PlayerData.pthis.iPlayTime / 3600, (PlayerData.pthis.iPlayTime / 60) % 60, PlayerData.pthis.iPlayTime % 60);
        // 殺怪數.
        pLb[2].text = PlayerData.pthis.iEnemyKill.ToString();

        AudioCtrl.pthis.pMusic.volume = 0.5f;
        NGUITools.PlaySound(Resources.Load("Sound/FX/Fail") as AudioClip);

        // 比較紀錄.
        ObjRecord.SetActive(RecordData.pthis.RecordNow());

        // 重設存檔與遊戲檔案.
        PlayerData.pthis.Clear();
        GameData.pthis.Clear();

		// 設為新遊戲.
		SysMain.pthis.NewRoleData();        
    }

    public void OnDestory()
    {
        AudioCtrl.pthis.pMusic.volume = 1f;
    }

    IEnumerator OpenPage()
    {
        while (pTitleBg.width < 1280)
        {
            pTitleBg.width += 27;
            yield return new WaitForEndOfFrame();
        }        
    }
}
