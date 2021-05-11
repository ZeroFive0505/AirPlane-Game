using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarField : MonoBehaviour
{

    private Transform thisTransform;
    private ParticleSystem.Particle[] points;
    private float fStarDistanceSqr;
    private float fStarClipDistanceSqr;

    public Color starColor;
    public int iStarMaximum = 500;
    public float fStarSize = 0.35f;
    public float fStarDistance = 60f;
    public float fStarClipDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        fStarDistanceSqr = fStarDistance * fStarDistance;
        fStarClipDistanceSqr = fStarClipDistance * fStarClipDistance;

    }

    private void CreateStars()
    {
        points = new ParticleSystem.Particle[iStarMaximum];
        
        for(int i = 0; i < iStarMaximum; i++)
        {
            points[i].position = Random.insideUnitSphere * fStarDistance + thisTransform.position;
            points[i].startColor = new Color(starColor.r, starColor.g, starColor.b, starColor.a);
            points[i].startSize = fStarSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (points == null)
            CreateStars();

        for (int i = 0; i < iStarMaximum; i++)
        {
            if ((points[i].position - thisTransform.position).sqrMagnitude > fStarDistanceSqr)
            {
                points[i].position =
                    Random.insideUnitSphere.normalized * fStarDistance + thisTransform.position;
            }

            if ((points[i].position - thisTransform.position).sqrMagnitude <= fStarClipDistanceSqr)
            {
                float percentage = (points[i].position - thisTransform.position).sqrMagnitude / fStarClipDistanceSqr;
                points[i].startColor = new Color(starColor.r, starColor.g, starColor.b, percentage);
                points[i].startSize = percentage * fStarSize;
            }
        }

        GetComponent<ParticleSystem>().SetParticles(points, points.Length);

    }
}
