using UnityEngine;
using System.Collections;

public class P_Recoed : MonoBehaviour 
{
    public UILabel[] pLb = new UILabel[4];

    void Start()
    {
        // 天數.
        pLb[0].text = "--";
        // 關卡時間.
        pLb[1].text = "--";
        // 殺怪數.
        pLb[2].text = "--";
        // 死亡人數.
        pLb[3].text = "--";

        int iRecCount = RecordData.pthis.Recordlist.Count;
        if (iRecCount <= 0)
            return;

        RecordData.pthis.Recordlist.Sort();

        // 天數.
        pLb[0].text = RecordData.pthis.Recordlist[iRecCount-1].iStage.ToString();
        // 關卡時間.
        pLb[1].text = string.Format("{0:00}:{1:00}:{2:00}", RecordData.pthis.Recordlist[iRecCount - 1].iPlayTime / 3600, (RecordData.pthis.Recordlist[iRecCount - 1].iPlayTime / 60) % 60, RecordData.pthis.Recordlist[iRecCount - 1].iPlayTime % 60);
        // 殺怪數.
        pLb[2].text = RecordData.pthis.Recordlist[iRecCount - 1].iEnemyKill.ToString();
        // 死亡人數.
        pLb[3].text = RecordData.pthis.Recordlist[iRecCount - 1].iPlayerLost.ToString();
    }
	
}
