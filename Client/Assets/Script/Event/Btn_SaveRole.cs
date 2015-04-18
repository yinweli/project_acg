using UnityEngine;
using System.Collections;

public class Btn_SaveRole : MonoBehaviour 
{
    public AIPlayer pPlayer;
    public Animator pAni;
    // ------------------------------------------------------------------
    void Start()
    {
        pPlayer = transform.parent.gameObject.GetComponent<AIPlayer>();
    }
    // ------------------------------------------------------------------
    void OnClick()
    {
        PlayerCreater.pthis.SaveRole(pPlayer.gameObject);
        Destroy(this);        
    }
    // ------------------------------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Look")
        {
            pAni.Play("TalkShing");
        }
    }
    // ------------------------------------------------------------------
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Look")
        {
            pAni.Play("Wait");
        }
    }
	
}
