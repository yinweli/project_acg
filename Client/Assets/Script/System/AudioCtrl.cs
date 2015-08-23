using UnityEngine;
using System.Collections;

public class AudioCtrl : MonoBehaviour {

    static public AudioCtrl pthis;

    public AudioSource pSound;
    public AudioSource pMusic;

    //public int iMusic;

    void Awake()
    {
        pthis = this;
        DontDestroyOnLoad(gameObject);
    }
	void Start()
	{
		if(PlayerPrefs.HasKey(GameDefine.szSaveMusic))
			pMusic.mute = PlayerPrefs.GetInt(GameDefine.szSaveMusic) > 0;

		if(PlayerPrefs.HasKey(GameDefine.szSaveSound))
			pSound.mute = PlayerPrefs.GetInt(GameDefine.szSaveSound) > 0;
	}
	void OnApplicationQuit()
	{
		PlayerPrefs.SetInt(GameDefine.szSaveMusic, pMusic.mute ? 1 : 0);
		PlayerPrefs.SetInt(GameDefine.szSaveSound, pSound.mute ? 1 : 0);
		PlayerPrefs.Save();
	}

    public void PlayMusic(string Name)
    {
        pMusic.Stop();

        AudioClip ClipBG = Resources.Load("Sound/" + Name) as AudioClip;
        pMusic.clip = ClipBG;

        pMusic.Play();
    }

    public void RedomMusic()
    {
        pMusic.Stop();

        pMusic.clip = Resources.Load(string.Format("Sound/BG_{0:000}", Random.Range(0, 8))) as AudioClip;
        //pMusic.clip = Resources.Load(string.Format("Sound/BG_{0:000}", iMusic)) as AudioClip;

        pMusic.Play();
    }

    public void Victory()
    {
        pMusic.Stop();
        pMusic.volume = 0.6f;
        pMusic.clip = Resources.Load("Sound/BG_Victory") as AudioClip;

        pMusic.Play();
    }
}
