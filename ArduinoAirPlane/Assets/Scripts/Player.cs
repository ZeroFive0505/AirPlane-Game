using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fPlayerMoveSpeed = 170.0f;
    public float fPlayerAccel = 0.0f;
    public GameManagerScrpit gameManagerScrpit;
    public GameObject ExplosionVFX;
    public AudioClip ExplosionSFX;

    private Rigidbody rigidBody;

    private float desireRot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        gameManagerScrpit = FindObjectOfType<GameManagerScrpit>();

        if(!rigidBody || !gameManagerScrpit)
        {
            Debug.LogError("Initialize was failed.");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManagerScrpit.HasGameStarted())
        {
            PlayerInput();
        }

   

        //Debug.Log(" X : " + Input.mousePosition.x + " Y : " + Input.mousePosition.y);

    }


    private void ResetRotation()
    {
        var desireRotQ = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, desireRot);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, desireRotQ, Time.deltaTime * 1.0f);
    }



    private void PlayerInput()
    {
        transform.position += transform.forward * Time.deltaTime * (fPlayerMoveSpeed + fPlayerAccel);
        //transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
        if(Input.GetAxis("Mouse Y")!= 0.0f || Input.GetAxis("Mouse X") != 0.0f)
        {
            transform.Rotate(-Input.GetAxis("Mouse Y"), 0.0f, -Input.GetAxis("Mouse X"));
        }
        else
        {
            ResetRotation();
        }


        if (Input.GetKey(KeyCode.A))
        {
            if (fPlayerAccel < 70.0f)
                fPlayerAccel += 2.0f;
        }
        else
        {
            if (fPlayerAccel > 0)
                fPlayerAccel -= 2.0f;
        }
    }

    public void PlayerDestroy()
    {
        GameObject VFX = Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        gameManagerScrpit.SetGameOver();
        Destroy(VFX, 2f);
        PlaySFX();
        FindObjectOfType<MusicManager>().PlayGameOver();
        Destroy(gameObject);
    }

    private void PlaySFX()
    {
        GameObject tempSFX = new GameObject("PlayerGameOver");
        AudioSource audioSource = tempSFX.AddComponent<AudioSource>();
        audioSource.clip = ExplosionSFX;
        audioSource.spatialBlend = 0.0f;
        audioSource.volume = 1.0f;
        audioSource.loop = false;
        audioSource.Play();
        Destroy(tempSFX, ExplosionSFX.length);
    }

}

