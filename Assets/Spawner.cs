using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacles;

    public GameManager gameManager;

    float time;
    float interval;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        interval = 2;


    }

    // Update is called once per frame
    void Update()
    {
        if (time > interval)
        {
            GameObject ob = Instantiate(obstacles[Random.Range(0, 5)]);
            ob.transform.localPosition = transform.position;
            ob.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));

            time = 0;
        }

        time += Time.deltaTime;
    }
}
