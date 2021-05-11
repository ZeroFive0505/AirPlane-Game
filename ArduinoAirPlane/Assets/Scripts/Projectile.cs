using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfs = new List<GameObject>();

    public AudioClip laserSFX;
  

    private GameObject effectToSpawn;
    private float fTimeToFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfs[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S) && Time.time >= fTimeToFire )
        {
            fTimeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fFireRate;
            SpawnVFX();
        }
    }

    void SpawnVFX()
    {
        GameObject vfx;
        if(firePoint != null)
        {
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            vfx.transform.rotation = firePoint.transform.rotation;
            PlaySFX();
        }
        else
        {
            Debug.Log("Fire Point should not be null");
        }
    }

    private void PlaySFX()
    {
        GameObject tempAudio = new GameObject("TempAudio");

        AudioSource audioSource = tempAudio.AddComponent<AudioSource>();

        audioSource.clip = laserSFX;

        audioSource.volume = 1.0f;

        audioSource.spatialBlend = 0.0f;

        audioSource.loop = false;

        audioSource.Play();

        Destroy(tempAudio, laserSFX.length);

    }
}
