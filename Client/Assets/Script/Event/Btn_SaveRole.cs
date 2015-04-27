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
        pPlayer = transform.parent.gameObject.GetComponent<AIPlayer>();
    }
    // ------------------------------------------------------------------
    void OnPress(bool bIsPress)
    {
        if (bIsPress)
        {
            NGUITools.PlaySound(Resources.Load("Sound/FX/SaveRole") as AudioClip);
            PlayerCreater.pthis.SaveRole(pPlayer.gameObject);
            GameData.pthis.PickupList[iItemID].bPickup = true;
            Destroy(gameObject);
        }
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Look")
            pAni.Play("TalkShing");
    }
    // ------------------------------------------------------------------
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Look")
            pAni.Play("Wait");
    }
	
}
