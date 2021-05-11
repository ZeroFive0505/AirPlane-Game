using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScrpit : MonoBehaviour
{
    public Player player;
    public Text GameStartTitle;
    public Text HowToStart;
    public Text Score;

    public Text GameOverTitle;
    public Text HowToRestart;

    public GameObject PlayerForRespawn;

    public MusicManager musicManager;
    public float fTargetTime = 2f;
    public float fTimeToSpawn = 5.0f;
    public bool bFadeSwitch = false;

    private bool bGameStart = false;
    private bool bTimeToSpawn = false;
    private bool bGameOver = false;
    private int IScore = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        musicManager = FindObjectOfType<MusicManager>();
        if(!player)
        {
            Debug.LogError("Player could not be found.");
        }
        musicManager.PlayIntro();
     
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
            transform.position = player.transform.position;


        if (Input.anyKey && !bGameStart)
        {
            bGameStart = true;
            GameStartTitle.enabled = false;
            HowToStart.enabled = false;
            musicManager.PlayBGM();
            Score.gameObject.SetActive(true);
            Score.text = "Score : " + IScore;
        }

        if(!bGameStart)
        {
            fTargetTime -= Time.deltaTime;
        }

        if (!bFadeSwitch && !bGameStart)
        {
            HowToStart.gameObject.SetActive(true);
        }
        else
        {
            HowToStart.gameObject.SetActive(false);
        }


        if(!bGameStart && fTargetTime <= 0)
        {
            fTargetTime = 2f;

            bFadeSwitch = !bFadeSwitch;
        }

        if(bGameStart && !bTimeToSpawn)
        {
            fTimeToSpawn -= Time.deltaTime;
        }

        if(fTimeToSpawn <= 0f && !bTimeToSpawn)
        {
            bTimeToSpawn = true;
        }

        if (bGameOver)
        {
            GameOverTitle.gameObject.SetActive(true);
            HowToRestart.gameObject.SetActive(true);
        }

        if (bGameOver && Input.anyKey)
        {
            RestartGame();
        }
    }

    public bool HasGameStarted()
    {
        return bGameStart;
    }

    public bool IsGameOver()
    {
        return bGameOver;
    }


    public bool IsTimeToSpawn()
    {
        return bTimeToSpawn;
    }

    public void IncreaseScore(int pScore)
    {
        IScore += pScore;
        Score.text = "Score : " + IScore;
    }

    public void SetGameOver()
    {
        bGameOver = true;
        bTimeToSpawn = false;
        GameOverTitle.gameObject.SetActive(true);
        HowToRestart.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        fTimeToSpawn = 5.0f;
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject asteroid in asteroids)
            Destroy(asteroid);
        GameOverTitle.gameObject.SetActive(false);
        HowToRestart.gameObject.SetActive(false);
        Instantiate(PlayerForRespawn, new Vector3(0, 0, 0), Quaternion.identity);
        GameStartTitle.gameObject.SetActive(true);
        HowToStart.gameObject.SetActive(true);
        player = FindObjectOfType<Player>();
        bGameOver = false;
        bGameStart = false;
        bTimeToSpawn = false;
        IScore = 0;
        FindObjectOfType<ChaseCam>().Restart();
        FindObjectOfType<Spawner>().ReStart();
        FindObjectOfType<MusicManager>().Restart();
    }
}
