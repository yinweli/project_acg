using UnityEngine;
using System.Collections;

public class P_Victory : MonoBehaviour 
{
    public GameObject[] ObjPage = new GameObject[3];
    public UILabel[] pLb = new UILabel[5];
	
    void Start()
    {
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

        SysMain.pthis.SaveGame();
    }

    public void ChangePage(int iPage)
    {
        ObjPage[iPage].SetActive(false);
        ObjPage[iPage + 1].SetActive(true);

        if (ObjPage[iPage + 1] && ObjPage[iPage + 1].GetComponent<G_Feature>())
            ObjPage[iPage + 1].GetComponent<G_Feature>().OpenPage();
    }
}
