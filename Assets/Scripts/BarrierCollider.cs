using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BarrierCollider : MonoBehaviour
{
    public GameManager gameManager;

    public AudioSource audio;

    public Text fuelText;

    bool inObstacle;

    private void Update()
    {
        //if(!inObstacle && audio.volume < 1)
        //{
        //    audio.volume += .02f;

        //    if (audio.volume > 1) audio.volume = 1;
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            gameManager.ChangeFuel(-1f);

            //if (audio.volume <= 0)
            //{
            //    audio.volume = 0;
            //}
            //else
            //{
            //    audio.volume -= .02f;
            //}

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Color damageColor = new Color(1f, .3f, .3f, 1f);

        if (other.gameObject.tag == "Obstacle")
        {
            fuelText.color = damageColor;
            inObstacle = true;
            gameManager.setCanScore(false);

            // audio.volume = .3f;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            fuelText.color = Color.white;
            inObstacle = false;
            gameManager.setCanScore(true);

            //audio.volume = 1f;
        }
        
    }
}
