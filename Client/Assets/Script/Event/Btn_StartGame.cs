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
    }
}
