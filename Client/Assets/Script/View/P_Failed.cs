using UnityEngine;
using System.Collections;

public class P_Failed : MonoBehaviour 
{
    public UISprite pTitleBg = null;
    public GameObject ObjRecord = null;
    public UILabel[] pLb = new UILabel[3];

    void Start()
    {
		GoogleAnalytics.pthis.LogEvent("Failed", "Day" + DataPlayer.pthis.iStage, "", 1);

        StartCoroutine(OpenPage());
        // 天數.
        pLb[0].text = DataPlayer.pthis.iStage.ToString();
        // 關卡時間.
        pLb[1].text = string.Format("{0:00}:{1:00}:{2:00}", DataPlayer.pthis.iPlayTime / 3600, (DataPlayer.pthis.iPlayTime / 60) % 60, DataPlayer.pthis.iPlayTime % 60);
        // 殺怪數.
        pLb[2].text = DataPlayer.pthis.iEnemyKill.ToString();

        AudioCtrl.pthis.pMusic.volume = 0.5f;
        NGUITools.PlaySound(Resources.Load("Sound/FX/Fail") as AudioClip);

        // 比較紀錄.
        ObjRecord.SetActive(DataRecord.pthis.RecordNow());

        // 重設存檔與遊戲檔案.
        DataPlayer.pthis.Clear();
        DataGame.pthis.Clear();
		DataPickup.pthis.Clear();

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
