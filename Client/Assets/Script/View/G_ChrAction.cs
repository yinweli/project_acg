using UnityEngine;
using System.Collections;

public class G_ChrAction : MonoBehaviour
{
    // 角色動畫.
    public Animator pAni = null;
    // 武器動畫.
    public Animator pWAni = null;
	// Use this for initialization
    // ------------------------------------------------------------------
	void Start ()
    {
        pAni = GetComponent<Animator>();
        PlayRun();
	}
    // ------------------------------------------------------------------
    public void PlayWait()
    {
        if (pAni)
            pAni.Play("wait");
    }
    // ------------------------------------------------------------------
    public void PlayRun()
    {
        if (pAni)
            pAni.Play("Run");
    }
    // ------------------------------------------------------------------
    public void PlayAtk(GameObject ObjTarget)
    {
        FaceTo(1, ObjTarget);

        if (pAni)
            pAni.Play("Shot");
        if (pWAni)
            pWAni.Play("Fire");	
    }
    // ------------------------------------------------------------------
    public void PlayBreak()
    {
        if (pAni)
            pAni.Play("Break");
    }
    // ------------------------------------------------------------------
    public void FaceTo(int iFaceTo, GameObject FaceObj)
    {
        if (transform.position.x < FaceObj.transform.position.x)
            transform.localScale = new Vector3(1 * iFaceTo, 1, 1);
        else
            transform.localScale = new Vector3(-1 * iFaceTo, 1, 1);
    }
    // ------------------------------------------------------------------
}
