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
			GoogleAnalytics.pthis.LogEvent("Count", "Save Member", "", 0);
            if (GameData.pthis.PickupList[iItemID] != null)
                GameData.pthis.PickupList[iItemID].bPickup = true;
            Destroy(gameObject);
        }
    }
}
