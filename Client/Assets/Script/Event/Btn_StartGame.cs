using UnityEngine;
using System.Collections;

public class Btn_StartGame : MonoBehaviour 
{
    public Animator pAni = null;

    void Start()
    {
        gameObject.collider2D.enabled = true;
    }    

	void OnClick()
    {
        gameObject.collider2D.enabled = false;
        pAni.Play("FadOut");
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.7f);
        SysMain.pthis.CheckStart();
    }
}
