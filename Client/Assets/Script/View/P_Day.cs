using UnityEngine;
using System.Collections;

public class P_Day : MonoBehaviour 
{
    public Animator pAni;
    public UILabel LbDay;

    void Start()
    {
        GoogleAnalyticsV3.getInstance().LogScreen("Stage");

        GoogleAnalyticsV3.getInstance().LogEvent("Play", "Day" + DataPlayer.pthis.iStage, "", 1);
        LbDay.text = "Day [e92121]" + DataPlayer.pthis.iStage;

        if(Rule.AppearBossStage())
            pAni.Play("ShowDayBoss");
    }
}
