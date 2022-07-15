using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public AudioSource aud;
    public AudioSource bgm;
    public AudioClip bossmusic;
    public AudioClip bg1;
    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }
    public void PlayClip(AudioClip ac)
    {
        aud.PlayOneShot(ac);
    }
    public void PlayBGM(AudioClip ac)
    {
        bgm.clip = ac;
        bgm.Play();
    }
}
