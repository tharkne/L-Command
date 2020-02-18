using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutofView : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < -40)
        {
            Destroy(gameObject);
        }
    }
}
