using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public Material[] obstacleMats;

    public Material[] materials;

    GameObject gm;
    GameManager gameManager;

    public GameObject pointBallFab;
    public GameObject fuelBallFab;

    float spawnTime;
    float interval;

    float gameTime;


    //    Vector3 up = new Vector3(0, 6, 0),
    //    Vector3 down = new Vector3(0, -6, 0),
    //    Vector3 left = new Vector3(-6, 0, 0),
    //    Vector3 right = new Vector3(6, 0, 0),

    private Vector3[] cardDirStart = new Vector3[]
    {
        new Vector3(0, 6, 0), // up
        new Vector3(0, -6, 0),  // down
        new Vector3(-6, 0, 0),  // left
        new Vector3(6, 0, 0),   // right
    };

    private Vector3[] auxDirStart = new Vector3[]
    {
        new Vector3(6, 6, 0),      // up_right
        new Vector3(-6, 6, 0),     // up_left 
        new Vector3(-6, -6, 0),    // down_left 
        new Vector3(6, -6, 0)      // down_right
    };

    

    //Vector3 up_right = new Vector3(6, 6, 0);
    //Vector3 up_left = new Vector3(-6, 6, 0);
    //Vector3 down_left = new Vector3(-6, -6, 0);
    //Vector3 down_right = new Vector3(6, -6, 0);

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0;
        gameTime = 0;
        interval = 2;

        gm = GameObject.Find("Game Manager");
        gameManager = gm.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTime > interval && !gameManager.checkGameOver())
        {
           
            SpawnObstacles();
            
            
            SpawnCollectables();


            spawnTime = 0;
        }

        gameTime = Time.time;
        spawnTime += Time.deltaTime;
    }

    void SpawnObstacles()
    {
        if (gameManager.getScore() > 500)
        {
            GameObject ob = Instantiate(obstacles[Random.Range(0, 7)]);
            ob.transform.localPosition = transform.position;


            ob.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));

            if(gameManager.getScore() > 1000)
            {
                //ob.GetComponent<MeshRenderer>().material = obstacleMats[Random.Range(0, 2)];
            }
        }
    }

    void SpawnCollectables()
    {
        // Spawn main balls

        if (gameManager.getScore() > 500)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject pointBall = Instantiate(pointBallFab);
                // assign ball a random material


                // place the ball in one of the four cardinal directions in relation to spawner
                pointBall.transform.position = cardDirStart[i] + transform.position;

                // pointBall.GetComponent<MeshRenderer>().Materials[0] = materials[Random.Range(0, 4)];
                pointBall.GetComponent<MeshRenderer>().material = materials[Random.Range(0, 4)];
            }
        }
        else
        {
            GameObject pointBall = Instantiate(pointBallFab);
            // assign ball a random material


            // place the ball in one of the four cardinal directions in relation to spawner
            pointBall.transform.position = cardDirStart[Random.Range(0, 4)] + transform.position;

            // pointBall.GetComponent<MeshRenderer>().Materials[0] = materials[Random.Range(0, 4)];
            pointBall.GetComponent<MeshRenderer>().material = materials[Random.Range(0, 4)];
        }




        if (gameManager.getScore() > 300)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject fuelBall = Instantiate(fuelBallFab);

                // place the ball in one of the four cardinal directions in relation to spawner
                fuelBall.transform.position = auxDirStart[i] + transform.position;
            }
        }
        else 
        {
            GameObject fuelBall = Instantiate(fuelBallFab);

            // place the ball in one of the four cardinal directions in relation to spawner
            fuelBall.transform.position = auxDirStart[Random.Range(0, 4)] + transform.position;
            
        }
    }
}
