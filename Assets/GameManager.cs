using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text fuelText;

    float fuelVal;
    float scoreVal;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        fuelVal = 100;
        scoreVal = 0;

        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > .5f)
        {
            CountDown();
            time = 0f;
        }

        scoreText.text = "Score " + scoreVal;
        fuelText.text = "Fuel " + fuelVal + "%";

        time += Time.deltaTime;
    }

    // loss of 2 fuel per second
    void CountDown()
    {
        ChangeFuel(-1f);
    }


    public void ChangeFuel(float change)
    {
        fuelVal += change;

        if(fuelVal > 100)
        {
            fuelVal = 100;
        }
        else if(fuelVal <= 0)
        {
            fuelVal = 0;
            StartCoroutine(GameOver());
        }
    }

    public void SetScore(float change)
    {
        scoreVal += change;
    }


    public IEnumerator GameOver()
    {

        yield return null;
    }
}
