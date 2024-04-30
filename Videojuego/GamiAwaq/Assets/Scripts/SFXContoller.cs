using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SFXContoller : MonoBehaviour
{
    public static SFXContoller Instance;

    public AudioSource Music;
    public AudioSource SFX;

    // Music
    public AudioClip Menu;
    public AudioClip Lobby;
    public AudioClip LobbyNight;
    public AudioClip Reptiles;
    public AudioClip Aves;
    public AudioClip Mamiferos;
    public AudioClip Rastros;
    public AudioClip Insectos;
    public AudioClip Vegetacion;
    public AudioClip GameFinish;
    public AudioClip Horizon;
    public AudioClip Enciclopedia;

    // SFX
     public AudioClip click;
    public AudioClip error;
    public AudioClip correct;
    public AudioClip encounter;
    public AudioClip countdownEnd;
    public AudioClip countdownStart;
    public AudioClip walk;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayClick()
    {
        SFX.clip = click;
        SFX.Play();
    }


    private void Start()
    {
        Music.clip = Menu;
        Music.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        Music.clip = clip;
        Music.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFX.clip = clip;
        SFX.Play();
    }

    public void Stop()
    {
        Music.Stop();
    }

    



}
