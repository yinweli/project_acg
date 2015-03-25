using UnityEngine;
using System.Collections;

public class AudioCtrl : MonoBehaviour {

    public AudioSource pSound;
    public AudioSource pMusic;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
