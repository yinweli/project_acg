using UnityEngine;
using System.Collections;

public class AudioCtrl : MonoBehaviour {

    static public AudioCtrl pthis;

    public AudioSource pSound;
    public AudioSource pMusic;

    void Awake()
    {
        pthis = this;
        DontDestroyOnLoad(gameObject);
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

        AudioClip ClipBG = Resources.Load(string.Format("Sound/BG_{0:000}", Random.Range(0, 3))) as AudioClip;
        pMusic.clip = ClipBG;

        pMusic.Play();
    }
}
