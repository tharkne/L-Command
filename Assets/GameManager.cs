using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text fuelText;
    public Text gameOverText;
    public Text infoText;

    public Text fuelPopUp;
    public Text scorePopUp;

    public AudioSource audio;

    public bool keyboardControls = false;

    float fuelVal;
    float scoreVal;

    float time;

    bool gameOver = false;

    float gameSpeed = 25;

    // the player CANNOT score when inside obstacles
    bool canScore = true;

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
        if (!gameOver)
        {
            if (time > .5f)
            {
                CountDown();
                time = 0f;
            }

            gameSpeed = 25 + (scoreVal / 100);

            scoreText.text = "Score " + scoreVal;
            fuelText.text = "Fuel " + fuelVal + "%";

            time += Time.deltaTime;
        }

        audio.volume = fuelVal / 100f;

        //if(Time.time > 5 && Time.time < 10)
        //{
        //    infoText.text = "Hover over White Fuel Cells to Regain Fuel";
        //}
        //else if(Time.time >= 10)
        //{
        //    infoText.text = "";
        //}
    }

    // loss of 2 fuel per second
    void CountDown()
    {
        ChangeFuel(-1f);
    }


    public void ChangeFuel(float change)
    {
        if (!gameOver)
        {

            if (change <= 0)
            {
                fuelVal += change;
            }
            // allow player to score if valid
            else if (change >= 0 && canScore)
            {
                fuelPopUp.text = "+" + change + "%";
                StartCoroutine(TextFade(fuelPopUp));
                fuelVal += change;
            }            

            if (fuelVal > 100)
            {
                fuelVal = 100;
            }
            else if (fuelVal <= 0)
            {
                fuelVal = 0;
                fuelText.text = "Fuel " + fuelVal + "%";
                StartCoroutine(GameOver());
            }
        }
    }

    public void ChangeScore(float change)
    {
        if (canScore && !gameOver)
        {
            scorePopUp.text = "+" + change;
            StartCoroutine(TextFade(scorePopUp));
            scoreVal += change;
        }
    }

    public IEnumerator TextFade(Text text)
    {
        float fadespeed = 1.0f / 2;

        Color c = text.color;
        c.a = 1f;

        for(float t = 0.0f; t < 1.0f; t += Time.deltaTime * fadespeed)
        {
            c.a = Mathf.Lerp(1, 0, t);
            text.color = c;
            yield return true;
        }

        yield return null;
    }


    private IEnumerator GameOver()
    {
        gameOver = true;
        gameOverText.text = "GAME OVER";

        StartCoroutine(TextFadeIn(gameOverText));

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("Menu");

        yield return null;
    }

    public IEnumerator TextFadeIn(Text text)
    {
        float fadespeed = 1.0f / 2;

        Color c = text.color;
        c.a = 0f;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * fadespeed)
        {
            c.a = Mathf.Lerp(0, 1, t);
            text.color = c;
            yield return true;
        }

        yield return null;
    }

    public float getSpeed()
    {
        return gameSpeed;
    }

    public float getScore()
    {
        return scoreVal;
    }

    public bool checkGameOver()
    {
        return gameOver;
    }

    public void setCanScore(bool trigger)
    {
        canScore = trigger;
    }
}
