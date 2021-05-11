using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameManagerScrpit gameManagerScrpit;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScrpit = FindObjectOfType<GameManagerScrpit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Asteroids>())
            gameManagerScrpit.IncreaseScore(2);
        Destroy(other.gameObject);
    }
}
