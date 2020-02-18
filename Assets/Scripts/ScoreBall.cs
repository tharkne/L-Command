using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using TMPro;

public class ScoreBall : MonoBehaviour
{
    GameObject gm;
    GameManager gameManager;

    AudioSource audio;

    public AudioClip[] boops;

    public GameObject ballTextField;


    GameObject ballText;

    Material mat;
    string matName;

    bool isText;

    // can't score on items in the barrier
    bool scorable = true;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        matName = mat.name;

        gm = GameObject.Find("Game Manager");
        gameManager = gm.GetComponent<GameManager>();

        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();



        ballText = Instantiate(ballTextField);
        CreateText(ballText);
    }

    // Update is called once per frame
    void Update()
    {
        if(ballText != null)
            ballText.transform.position = transform.position + new Vector3(.4f ,0, -1.3f);


        // Debug.Log(Gamepad.current.xButton.wasPressedThisFrame);
    }

    void CreateText(GameObject ballText)
    {
        ballText.GetComponent<TMP_Text>().fontSize = 25;
        //ballText.GetComponent<TMP_Text>().alignment

        ballText.transform.position = transform.position + new Vector3(.4f, 0, -1.3f);

        if (matName.Contains("Green"))
        {
            ballText.GetComponent<TMP_Text>().text = "A";
        }
        else if (matName.Contains("Blue"))
        {
            ballText.GetComponent<TMP_Text>().text = "X";
        }
        else if (matName.Contains("Red"))
        {
            ballText.GetComponent<TMP_Text>().text = "B";
        }
        else if (matName.Contains("Yellow"))
        {
            ballText.GetComponent<TMP_Text>().text = "Y";
        }
    }



    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("Player entered");
        if(other.gameObject.tag == "Player")
        {
            CheckScore(other.gameObject);
        }
        else if(other.gameObject.tag == "Obstacle")
        {
            scorable = false;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Obstacle")
    //    {
    //        scorable = false;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Obstacle")
    //    {
    //        scorable = true;
    //    }
    //}




    void CheckScore(GameObject player)
    {
        Gamepad activeGamepad = Gamepad.current;
        //Debug.Log(matName);

        if (matName.Contains("Green"))
        {
            if (activeGamepad.aButton.wasPressedThisFrame)
            {
                OnCollect(player, 3);
                
            }
        }
        else if (matName.Contains("Blue"))
        {
            if (activeGamepad.xButton.wasPressedThisFrame)
            {
                OnCollect(player, 1);
            }
        }
        else if (matName.Contains("Red")) {
            if (activeGamepad.bButton.wasPressedThisFrame)
            {
                OnCollect(player, 0);
            }
        }
        else if (matName.Contains("Yellow")) {
           
            if (activeGamepad.yButton.wasPressedThisFrame)
            {
                //Debug.Log();

                OnCollect(player, 2);
            } 
        }
    }

    void OnCollect(GameObject player, int boopNum)
    {
        if (scorable)
        {
            CalcScore(Vector3.Distance(transform.position, player.transform.position));

            audio.PlayOneShot(boops[boopNum], .15f);

            this.gameObject.SetActive(false);
            Destroy(ballText);
        }
    }

    void CalcScore(float distance)
    {
        int baseline = (int)(100 - distance * 10);

        if(baseline < 5)
        {
            baseline = 5;
        }

        gameManager.ChangeScore(baseline);
        gameManager.ChangeFuel(2f);
    }
}


