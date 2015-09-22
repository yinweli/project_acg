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
            pAni.Play("Run",-1,0);
    }
    // ------------------------------------------------------------------
    public void PlayAtk(GameObject ObjTarget)
    {
        FaceTo(1, ObjTarget);

        if (pAni)
            pAni.Play("Shot", -1, 0);
        if (pWAni)
            pWAni.Play("Fire", -1, 0);	
    }
    // ------------------------------------------------------------------
    public void PlayBreak()
    {
        if (pAni)
            pAni.Play("Break");
    }
    // ------------------------------------------------------------------
    public void PlayCharm()
    {
        if (pAni)
            pAni.Play("Charm", -1, 0);
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
