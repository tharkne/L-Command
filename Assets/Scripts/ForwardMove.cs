using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMove : MonoBehaviour
{
    GameObject gm;
    GameManager gameManager;


    public float speed = 25;

    private Vector3 towardCamera = new Vector3(0, 0, -1);

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager");
        gameManager = gm.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = gameManager.getSpeed();
        transform.position += Time.deltaTime * towardCamera * speed;
    }
}
