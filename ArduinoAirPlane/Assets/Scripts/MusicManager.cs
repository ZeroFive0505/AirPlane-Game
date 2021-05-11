using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Player player;
    public GameManagerScrpit gameManagerScrpit;

    public AudioClip IntroClip;
    public AudioClip BGM;
    public AudioClip GameOver;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
        gameManagerScrpit = FindObjectOfType<GameManagerScrpit>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            transform.position = player.transform.position; // Follow Player
    }

    public void PlayIntro()
    {
        audioSource.clip = IntroClip;
        audioSource.volume = 0.8f;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayBGM()
    {
        audioSource.clip = BGM;
        audioSource.volume = 0.8f;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayGameOver()
    {
        audioSource.clip = GameOver;
        audioSource.volume = 1.0f;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void Restart()
    {
        PlayIntro();
        player = FindObjectOfType<Player>();
    }
}
