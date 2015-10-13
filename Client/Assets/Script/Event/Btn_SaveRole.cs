using UnityEngine;
using System.Collections;

public class Btn_SaveRole : MonoBehaviour 
{
    public int iItemID = 0;

    public AIPlayer pPlayer;
    public Animator pAni;
    // ------------------------------------------------------------------
    void Start()
    {
        pAni.Play("TalkShing");
    }
    // ------------------------------------------------------------------
    void OnPress(bool bIsPress)
    {
        if (bIsPress)
        {
            NGUITools.PlaySound(Resources.Load("Sound/FX/SaveRole") as AudioClip);
            if (pPlayer.iTied <= 0)
            {
                PlayerCreater.pthis.SaveRole(pPlayer.gameObject);
                Statistics.pthis.RecordResource(ENUM_Pickup.Member, 1);
                GoogleAnalytics.pthis.LogEvent("Count", "Save Member", "", 0);
                if (DataPickup.pthis.Data[iItemID] != null)
                    DataPickup.pthis.Data[iItemID].bPickup = true;
            }            
            Destroy(gameObject);
        }
    }
}
