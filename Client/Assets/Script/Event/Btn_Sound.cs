using UnityEngine;
using System.Collections;

public class Btn_Sound : MonoBehaviour 
{
    public GameObject pObj;
    public UISprite pBg;
    // Use this for initialization
    void Start()
    {
        pObj.SetActive(AudioCtrl.pthis.pSound.mute);

        if (AudioCtrl.pthis.pSound.mute)
            pBg.color = Color.gray;
        else
            pBg.color = Color.white;
    }

    void OnClick()
    {
        AudioCtrl.pthis.pSound.mute = !AudioCtrl.pthis.pSound.mute;

        pObj.SetActive(AudioCtrl.pthis.pSound.mute);

        if (AudioCtrl.pthis.pSound.mute)
            pBg.color = Color.gray;
        else
            pBg.color = Color.white;
    }
}
