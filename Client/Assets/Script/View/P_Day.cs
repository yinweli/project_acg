using UnityEngine;
using System.Collections;

public class P_Day : MonoBehaviour 
{
    public Animator pAni;
    public UILabel LbDay;

    void Start()
    {
        GoogleAnalytics.pthis.LogEvent("Play", "Day" + DataPlayer.pthis.iStage, "", 1);
        LbDay.text = "Day [e92121]" + DataPlayer.pthis.iStage;

        if (DataPlayer.pthis.iStage % GameDefine.iBossStage == 0)
            pAni.Play("ShowDayBoss");
    }
}
