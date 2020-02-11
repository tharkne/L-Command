using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class StickMovement : MonoBehaviour
{
    Rigidbody rb;
    public float outSpeed;
    public float returnSpeed;
    public float strafeSpeed;

    private Vector3 basePosition;

    private bool inBounds;

    // allows user to strafe when outside the bounds
    private bool hStrafe;
    private bool vStrafe;

    // records position of joystick at start of strafe engage
    // so it can be cancelled if user inputs another direction
    
    private float hStrafeActivationDirection;
    private float vStrafeActivationDirection;

    // active when cancelling a strafe
    private bool returning;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        inBounds = true;
        returning = false;
    }

    // Update is called once per frame
    void Update()
    {
        List<Gamepad> game_pads = new List<Gamepad>(Gamepad.all);

        foreach (Gamepad gp in game_pads)
        {
         //   Debug.Log(gp.name);
        }

        basePosition = Vector3.zero;

        //transform.position = new Vector3(horiz * 6, vert * 6, 0);
    }

    private void FixedUpdate()
    {
        Gamepad active_gamepad = Gamepad.current;

        float horiz = active_gamepad.leftStick.x.ReadValue();
        float vert = active_gamepad.leftStick.y.ReadValue();

        bool a_pressed_this_frame = active_gamepad.aButton.wasPressedThisFrame;
        bool b_pressed_this_frame = active_gamepad.bButton.wasPressedThisFrame;

        // List<Gamepad> game_pads = new List<Gamepad>(Gamepad.all);


        

        // when no direction on stick pressed
        if (Mathf.Abs(horiz) < 0.05f && Mathf.Abs(vert) < 0.05f)
        {
            // return to base position
            if (Vector3.Distance(Vector3.zero, transform.position) <= 1f)
            {
                transform.position = Vector3.zero;
                rb.velocity = Vector3.zero;
            }
            else
            {
                rb.velocity = returnSpeed * Vector3.Normalize(basePosition - transform.position);
            }
        }
        else if (inBounds)
        {
            // cardinal direction movement
            if (Mathf.Abs(horiz) > .8)
                horiz = 1.0f * Mathf.Sign(horiz);
            else
                horiz = 0f;

            if (Mathf.Abs(vert) > .8)
                vert = 1.0f * Mathf.Sign(vert);
            else
                vert = 0f;


            rb.velocity = outSpeed * new Vector3(horiz, vert, 0);
        }
        else if(!inBounds)
        {
            if (hStrafe)
            {
                // cancel strafe and unlock bound if user changes stick direction
                if(Mathf.Abs(vert - hStrafeActivationDirection) >= 1.3 && Mathf.Abs(transform.position.x) < 6)
                {
                    Debug.Log("Horizontal Strafe Deactivated");
                    hStrafe = false;
                    // inBounds = true;
                    rb.velocity = returnSpeed * Vector3.Normalize(basePosition - transform.position);

                    returning = true;
                }
                else
                {

                    vert = 0f;


                    if (Mathf.Abs(transform.position.x) > 6)
                    {
                        rb.velocity = -.001f * rb.velocity;
                    }
                    else if (Mathf.Abs(horiz) > .7)
                    {   
                        horiz = 1.0f * Mathf.Sign(horiz);
                        rb.velocity = strafeSpeed * new Vector3(horiz, vert, 0);
                    }
                    else
                    {
                        rb.velocity = strafeSpeed * Vector3.Normalize(new Vector3(0, 6 * hStrafeActivationDirection, 0) - transform.position);
                    }




                }
            }
            else if (vStrafe)
            {

                // cancel strafe and unlock bound if user changes stick direction
                if (Mathf.Abs(horiz - vStrafeActivationDirection) >= 1.3 && Mathf.Abs(transform.position.y) < 6)
                {
                    Debug.Log("Vertical Strafe Deactivated");
                    vStrafe = false;
                    // inBounds = true;
                    rb.velocity = returnSpeed * Vector3.Normalize(basePosition - transform.position);

                    returning = true;
                }
                else
                {
                    Debug.Log("Vertical Strafe");

                    horiz = 0f;


                    if (Mathf.Abs(transform.position.y) > 6)
                    {
                        rb.velocity = -.001f * rb.velocity;
                    }
                    else if (Mathf.Abs(vert) > .7)
                    {
                        vert = 1.0f * Mathf.Sign(vert);
                        rb.velocity = strafeSpeed * new Vector3(horiz, vert, 0);
                    }
                    else
                    {
                        rb.velocity = strafeSpeed * Vector3.Normalize(new Vector3(6 * vStrafeActivationDirection, 0, 0) - transform.position);
                    }


                    

                    
                }
            }
            else if (returning)
            {
                rb.velocity = returnSpeed * Vector3.Normalize(basePosition - transform.position);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }

        // Debug.Log(transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Bound Box")
        {
            Debug.Log("Left Game area");
            inBounds = false;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        // Mark character as 'In Bounds' once they return into the bounding box
        if (!inBounds && other.gameObject.tag == "Bound Box")
        {
            inBounds = true;
            hStrafe = false;
            vStrafe = false;

            returning = false;
        }


        switch (other.gameObject.tag)
        {
            case "Up":
                hStrafe = true;
                hStrafeActivationDirection = 1;
                break;
            case "Down":
                hStrafe = true;
                hStrafeActivationDirection = -1;
                break;
            case "Left":
                vStrafe = true;
                vStrafeActivationDirection = -1;
                break;
            case "Right":
                vStrafe = true;
                vStrafeActivationDirection = 1;
                break;

        }
    }
}
