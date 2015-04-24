using UnityEngine;
using System.Collections;

public class P_Failed : MonoBehaviour 
{
    public UISprite pTitleBg;
    public UILabel[] pLb = new UILabel[3];

    void Start()
    {
        StartCoroutine(OpenPage());
        // 天數.
        pLb[0].text = PlayerData.pthis.iStage.ToString();
        // 關卡時間.
        pLb[1].text = string.Format("{0:00}:{1:00}:{2:00}", PlayerData.pthis.iPlayTime / 3600, (PlayerData.pthis.iPlayTime / 60) % 60, PlayerData.pthis.iPlayTime % 60);
        // 殺怪數.
        pLb[2].text = PlayerData.pthis.iEnemyKill.ToString();

        // 重設存檔與遊戲檔案.
        PlayerData.pthis.ClearData();
        GameData.pthis.ClearData();

        SysMain.pthis.SaveGame();
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
