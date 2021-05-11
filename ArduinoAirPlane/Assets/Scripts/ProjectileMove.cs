using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float fProjectileSpeed;
    public float fFireRate;

    public GameObject muzzleFlash;
    public GameObject hitFlash;

    private Projectile projectile;
    private GameObject muzzleVFX;

    private float damage = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        projectile = FindObjectOfType<Projectile>();
        if(muzzleFlash!= null)
        {
            muzzleVFX = Instantiate(muzzleFlash, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
                Destroy(muzzleVFX, psMuzzle.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fProjectileSpeed != 0)
        {
            transform.position += transform.forward * fProjectileSpeed * Time.deltaTime;
        }
        else
        {
            Debug.Log("No speed");
        }

        if(muzzleVFX != null)
        {
            if(projectile.firePoint)
                muzzleVFX.transform.position = projectile.firePoint.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Asteroids>())
        {
            collision.gameObject.GetComponent<Asteroids>().DealDamage(damage);
            ContactPoint contactPoint = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
            Vector3 pos = contactPoint.point;

            if (hitFlash != null)
            {
                GameObject hitVFX = Instantiate(hitFlash, pos, rot);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                    Destroy(hitVFX, psHit.main.duration);
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }
            Destroy(gameObject);
        }

    }
}
