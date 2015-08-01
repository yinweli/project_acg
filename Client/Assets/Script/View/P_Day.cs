using UnityEngine;
using System.Collections;

public class P_Day : MonoBehaviour 
{
    public Animator pAni;
    public UILabel LbDay;

    public void SetDay()
    {
		GoogleAnalytics.pthis.LogEvent("Play", "Day" + DataPlayer.pthis.iStage, "", 1);
        StartCoroutine(WaitFad());
        LbDay.text = "Day [e92121]" + DataPlayer.pthis.iStage;
    }

    IEnumerator WaitFad()
    {
        yield return new WaitForSeconds(2.5f);
        pAni.Play("FadOut");
    }
}
