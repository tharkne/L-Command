using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    float totalSpin = 0;

    Material purpleMat;


    // bool stopSpin = false;

    float direction = 0;
    int rotationPicker = 0;

    float totalRotation = 0;

    float gameTime = 0;

    float rotateWait = 0;

    GameObject gm;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager");
        gameManager = gm.GetComponent<GameManager>();


        if (Random.Range(0,2) == 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        rotationPicker = Random.Range(0, 2);

        switch (rotationPicker)
        {
            case 0:
                totalRotation = 180;
                break;
            case 1:
                totalRotation = 90;
                break;
        }

        gameTime = 0;


        purpleMat = (Material)Resources.Load("Danger Purple", typeof(Material));

        if (totalRotation == 180 && gameManager.getScore() > 1700)
        {
            if (gameManager.getScore() > 3400)
            {
                totalRotation = 90;
            }
            else
            {
                foreach (Transform child in transform)
                {
                    foreach (MeshRenderer obMesh in child.GetComponentsInChildren<MeshRenderer>())
                    {
                        obMesh.material = purpleMat;
                    }
                }
            }
        }

        rotateWait = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // rotateWait += Time.deltaTime;

        if (gameManager.getScore() > 1700)
        {

            if (totalSpin <= totalRotation)
            {
                transform.Rotate(0, 0, direction * 3, Space.World);
                totalSpin += 3;
            }

            //Material pinkMat = (Material)Resources.Load("Danger Pink", typeof(Material));

            //if (totalRotation == 180 && totalSpin > 90)
            //{
            //    foreach (Transform child in transform)
            //    {
            //        foreach (MeshRenderer obMesh in child.GetComponentsInChildren<MeshRenderer>())
            //        {
            //            obMesh.material = pinkMat;
            //        }
            //    }
            //}
        }

    }
}
