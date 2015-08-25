using UnityEngine;
using System.Collections;

public class G_Recoed : MonoBehaviour 
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

        int iRecCount = DataRecord.pthis.Data.Count;

        if (iRecCount <= 0)
            return;

        DataRecord.pthis.Data.Sort();

        // 天數.
        pLb[0].text = DataRecord.pthis.Data[iRecCount-1].iStage.ToString();
        // 關卡時間.
        pLb[1].text = string.Format("{0:00}:{1:00}:{2:00}", DataRecord.pthis.Data[iRecCount - 1].iPlayTime / 3600, (DataRecord.pthis.Data[iRecCount - 1].iPlayTime / 60) % 60, DataRecord.pthis.Data[iRecCount - 1].iPlayTime % 60);
        // 殺怪數.
        pLb[2].text = DataRecord.pthis.Data[iRecCount - 1].iEnemyKill.ToString();
        // 死亡人數.
        pLb[3].text = DataRecord.pthis.Data[iRecCount - 1].iPlayerLost.ToString();
    }
	
}
