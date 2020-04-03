using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControllerButtons : MonoBehaviour
{
    Gamepad playerPad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPad = Gamepad.current;

        if (playerPad != null)
        {
            if (playerPad.startButton.wasPressedThisFrame)
            {
                SceneManager.LoadScene("SampleScene");
            }
            else if (playerPad.selectButton.wasPressedThisFrame)
            {
                Application.Quit(0);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("SampleScene");
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit(0);
            }
        }
    }
}
