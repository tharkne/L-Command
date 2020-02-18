using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBall : MonoBehaviour
{
    GameObject gm;
    GameManager gameManager;

    bool scorable;
    

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager");
        gameManager = gm.GetComponent<GameManager>();


        scorable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && scorable)
        {
            gameManager.ChangeFuel(10f);
            gameManager.ChangeScore(10f);
            Destroy(gameObject);
        }
    }

    //private void OnBecameVisible()
    //{
    //    Debug.Log("Scorable");
    //    scorable = true;
    //}

    //private void OnBecameInvisible()
    //{
    //    Debug.Log("Not Scorable");
    //    scorable = false;
    //}

 
    
}
