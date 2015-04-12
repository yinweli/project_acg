using UnityEngine;
using System.Collections;

public class P_Victory : MonoBehaviour 
{
    public UILabel[] pLb = new UILabel[5];
	
    void Start()
    {
        // 天數.
        pLb[0].text = PlayerData.pthis.iStage.ToString();
        // 關卡時間.
        pLb[1].text = string.Format("{0:00}:{1:00}:{2:00}",GameData.pthis.iStageTime / 3600 ,GameData.pthis.iStageTime % 3600, GameData.pthis.iStageTime % 60);
        // 殺怪數.
        pLb[2].text = GameData.pthis.iKill.ToString();
    }
}
