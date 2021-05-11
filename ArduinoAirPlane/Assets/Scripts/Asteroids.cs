using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField]
    float minScale = 0.8f;
    [SerializeField]
    float maxScale = 1.2f;
    [SerializeField]
    float rotationOffSet = 100f;
    [SerializeField]
    float fAsteroidSpeed = 10.0f;


    public float minOffSet = -1.0f;
    public float maxOffSet = 1.0f;

    public float minSpeed = 150.0f;
    public float maxSpeed = 200.0f;

    public GameObject ExplosionVFX;
    public AudioClip ExplosionSFX;

    public float Health;

   

    private Transform MyT;
    private Rigidbody rigidbody;

    public Vector3 randomRotation;
    public Vector3 Direction;
    public Vector3 RandomOffSet;



    private Player player;
    private GameManagerScrpit gameManagerScrpit;

    private const float FramesCount = 3f;
    private const float FAnimationTime = 1.0f;
    private float FDeltaTime = FAnimationTime / FramesCount;
    private float FDx;
    private float FCurrentScale = 1.0f;
    private float fTargetScale;
    

    private void Awake()
    {
        MyT = transform;
        rigidbody = GetComponent<Rigidbody>();
        gameManagerScrpit = FindObjectOfType<GameManagerScrpit>();

    }

    private void Start()
    {
        Health = Random.Range(100, 300);
        player = FindObjectOfType<Player>();
        fTargetScale = Random.Range(minScale, maxScale);
        FDx = (fTargetScale - 1) / FramesCount;
        StartCoroutine(SizeUp());



        randomRotation.x = Random.Range(-rotationOffSet, rotationOffSet);
        randomRotation.y = Random.Range(-rotationOffSet, rotationOffSet);
        randomRotation.z = Random.Range(-rotationOffSet, rotationOffSet);

       
        Direction = player.transform.position - transform.position;
        Direction.Normalize();
        fAsteroidSpeed = Random.Range(minSpeed, maxSpeed);
        
        
        rigidbody.AddForce(Direction * fAsteroidSpeed, ForceMode.VelocityChange);
    }



    // Update is called once per frame
    void Update()
    {
        MyT.Rotate(randomRotation * Time.deltaTime);

    }

    private IEnumerator SizeUp()
    {
        FCurrentScale += FDx;

        if(FCurrentScale < fTargetScale)
        {
            transform.localScale = Vector3.one * FCurrentScale;
            yield return new WaitForSeconds(FDeltaTime);
        }
        
    }


    public void DealDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            gameManagerScrpit.IncreaseScore(100);
            GameObject VFX = Instantiate(ExplosionVFX, transform.position, transform.rotation);
            PlaySFX();
            Destroy(VFX, 2f);
            Destroy(gameObject);
        }
    }

    private void PlaySFX()
    {
        GameObject tempExSFX = new GameObject("TempExSFX");
        AudioSource audioSource = tempExSFX.AddComponent<AudioSource>();
        audioSource.clip = ExplosionSFX;
        audioSource.volume = 1f;
        audioSource.spatialBlend = 0f;
        audioSource.loop = false;
        audioSource.Play();
        Destroy(tempExSFX, ExplosionSFX.length);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            collision.gameObject.GetComponent<Player>().PlayerDestroy();
    }
}
