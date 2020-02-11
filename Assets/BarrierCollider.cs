using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCollider : MonoBehaviour
{
    public GameManager gameManager;


    private void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            gameManager.ChangeFuel(-1f);
        }
    }
}
