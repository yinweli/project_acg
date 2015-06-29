using UnityEngine;
using System.Collections;

public class P_Day : MonoBehaviour 
{
    public Animator pAni;
    public UILabel LbDay;

    public void SetDay()
    {
		GoogleAnalytics.pthis.LogEvent("Count", "Play Day" + PlayerData.pthis.iStage, "", 1);
        StartCoroutine(WaitFad());
        LbDay.text = "Day [e92121]" + PlayerData.pthis.iStage;
    }

    IEnumerator WaitFad()
    {
        yield return new WaitForSeconds(2.5f);
        pAni.Play("FadOut");
    }
}
