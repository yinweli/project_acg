using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Btn_OpenAds : MonoBehaviour {

	void OnClick()
    {
        if (Advertisement.isReady())
            Advertisement.Show();
    }
}
