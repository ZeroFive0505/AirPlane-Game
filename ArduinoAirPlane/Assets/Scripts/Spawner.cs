using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Asteroids;
    public float fSizeX = 400;
    public float fSizeY = 400;
    public float fSizeZ = 100;
    private Player player;
    private GameManagerScrpit gameManagerScrpit;
    public float fFrquency = 40.0f;
    private float fTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameManagerScrpit = FindObjectOfType<GameManagerScrpit>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScrpit.IsTimeToSpawn() && !gameManagerScrpit.IsGameOver())
        {
            if (fTimer < fFrquency)
                fTimer += 5.0f;
            else
            {
                SpawnAsteroid();
                fTimer = 0.0f;
            }
        }

        if(player != null)
        {
            transform.rotation = player.transform.rotation;
            transform.position = player.transform.forward * 700 + player.transform.position;
        }
    }


    public void SpawnAsteroid()
    {
        int index = Random.Range(0, 3);
        GameObject obj;
        Vector3 Pos = transform.position + new Vector3(Random.Range(-fSizeX / 2, fSizeX / 2), Random.Range(-fSizeY / 2, fSizeY / 2), Random.Range(-fSizeZ / 2, fSizeZ / 2));
        obj = Instantiate(Asteroids[index], Pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, new Vector3(fSizeX, fSizeY, fSizeZ));
    }

    public void ReStart()
    {
        player = FindObjectOfType<Player>();
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.forward * 700 + player.transform.position;
    }
}
