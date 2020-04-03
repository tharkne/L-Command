using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using TMPro;

public class QuitTextDriver : MonoBehaviour
{
    Gamepad playerPad;

    // Start is called before the first frame update
    void Start()
    {
        // playerPad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        playerPad = Gamepad.current;

        if (playerPad != null)
        {
            GetComponent<TMP_Text>().text = "Press 'Select' to Quit";
        }
        else
        {
            GetComponent<TMP_Text>().text = "Press Escape to Quit";
        }
    }
}

