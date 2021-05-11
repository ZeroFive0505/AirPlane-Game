using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCam : MonoBehaviour
{
    public Player player;

    public float fCamPosX = 10.0f;
    public float fCamPosY = 5.0f;
    public float fCamLookAheadX = 30.0f;

    public Vector3 velocity = Vector3.zero;
    public float fBias = 0.75f;

    private Vector3 InitialPos = new Vector3(0.0f, 18.5f, -100.0f);

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fBias > -0.3)
            fBias -= 0.01f;

        if(player!=null)
        {
            Vector3 moveCamTo = player.transform.position - player.transform.forward * fCamPosX + Vector3.up * fCamPosY;
            transform.position = Vector3.SmoothDamp(transform.position, transform.position * fBias + moveCamTo * (1.0f - fBias), ref velocity, 0.15f);
            transform.LookAt(player.transform.position + player.transform.forward * fCamLookAheadX);
        }
    }

    public void Restart()
    {
        transform.position = InitialPos;
        player = FindObjectOfType<Player>();
        fBias = 0.75f;
    }
}
