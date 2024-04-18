using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXContoller : MonoBehaviour
{
    public static SFXContoller Instance;

    public AudioClip grillos;
    public AudioClip pajaros;
    public AudioClip lofi;
    public AudioClip click;

    public AudioSource src;

    public void Start()
    {
        PlayLofi();
    }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    public void PlayGrillos()
    {
        src.clip = grillos;
        src.Play();
    }

    public void PlayLofi()
    {
        src.clip = lofi;
        src.Play();
    }

    public void PlayPajaros()
    {
        src.clip = pajaros;
        src.Play();
    }

    public void PlayClick()
    {
        src.clip = click;
        src.Play();
    }

    public void Stop()
    {
        src.Stop();
    }



}
