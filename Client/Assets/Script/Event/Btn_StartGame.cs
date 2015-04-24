using UnityEngine;
using System.Collections;

public class Btn_StartGame : MonoBehaviour 
{
    public Animator pAni = null;

    void Start()
    {
        GetComponent<UIButton>().isEnabled = true;
    }    

	void OnClick()
    {
        GetComponent<UIButton>().isEnabled = false;
        pAni.Play("FadOut");
    }
}
