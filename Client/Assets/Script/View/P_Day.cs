using UnityEngine;
using System.Collections;

public class P_Day : MonoBehaviour 
{
    public Animator pAni;
    public UILabel LbDay;

    public void SetDay()
    {
        pAni.Play("FadIn");
        LbDay.text = "Day " + PlayerData.pthis.iStage;    
    }
}
