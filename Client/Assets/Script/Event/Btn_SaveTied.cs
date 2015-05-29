using UnityEngine;
using System.Collections;

public class Btn_SaveTied : MonoBehaviour 
{
    public AIPlayer pPlayer;
    public Animator pAni;
    // ------------------------------------------------------------------
    void Start()
    {
        pPlayer = transform.parent.gameObject.GetComponent<AIPlayer>();
    }
    // ------------------------------------------------------------------
    void Update()
    {
		if (pPlayer != null && EnemyCreater.pthis.CheckPos(pPlayer.gameObject))
            pPlayer.BeKill();
    }
    // ------------------------------------------------------------------
    void OnPress(bool bIsPress)
    {
        if (bIsPress)
        {
            NGUITools.PlaySound(Resources.Load("Sound/FX/SaveRole") as AudioClip);            
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
