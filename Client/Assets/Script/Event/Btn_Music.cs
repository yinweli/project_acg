using UnityEngine;
using System.Collections;

public class Btn_Music : MonoBehaviour 
{
    public GameObject pObj;
    public UISprite pBg;
	// Use this for initialization
	void Start () 
    {
        pObj.SetActive(AudioCtrl.pthis.pMusic.mute);

        if (AudioCtrl.pthis.pMusic.mute)
            pBg.color = Color.gray;
	}

    void OnClick()
    {
        AudioCtrl.pthis.pMusic.mute = !AudioCtrl.pthis.pMusic.mute;

        pObj.SetActive(AudioCtrl.pthis.pMusic.mute);

        if (AudioCtrl.pthis.pMusic.mute)
            pBg.color = Color.gray;
        else
            pBg.color = Color.white;
    }
}
