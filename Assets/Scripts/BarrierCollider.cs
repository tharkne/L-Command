using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BarrierCollider : MonoBehaviour
{
    public GameManager gameManager;

    public AudioSource audio;

    public Text fuelText;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            gameManager.ChangeFuel(-1f);
            
        }
        else
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Color damageColor = new Color(1f, .3f, .3f, 1f);

        if (other.gameObject.tag == "Obstacle")
        {
            fuelText.color = damageColor;
            Debug.Log("Cannot Score");
            gameManager.setCanScore(false);

            audio.volume = .3f;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            fuelText.color = Color.white;
            Debug.Log("Can Score");
            gameManager.setCanScore(true);

            audio.volume = 1f;
        }
        
    }
}
