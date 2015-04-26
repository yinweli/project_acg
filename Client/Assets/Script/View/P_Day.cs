using UnityEngine;
using System.Collections;

public class P_Day : MonoBehaviour 
{
    public Animator pAni;
    public UILabel LbDay;

    void Update()
    {
        if (GetComponent<UIPanel>().alpha < 0.002)
            Destroy(gameObject);
    }

    public void SetDay()
    {
        //GoogleAnalyticsV3.instance.LogScreen("Day " + PlayerData.pthis.iStage);
        pAni.Play("FadIn");
        LbDay.text = "Day " + PlayerData.pthis.iStage;
    }
}
