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
            PlayerCreater.pthis.SaveRole(pPlayer.gameObject);
			PickupStat.pthis.Record(ENUM_Pickup.Member, 1);
			GoogleAnalytics.pthis.LogEvent("Count", "Save Member", "", 0);
            if (DataGame.pthis.PickupList[iItemID] != null)
                DataGame.pthis.PickupList[iItemID].bPickup = true;
            Destroy(gameObject);
        }
    }
}
