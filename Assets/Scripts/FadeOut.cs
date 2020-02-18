using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private Color alphaColor;

    private MeshRenderer mr;

    private float fadeDuration = .1f;

    private bool fading;

    // Start is called before the first frame update
    void Start()
    {
        alphaColor = GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;

        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < 0 && !fading)
        {
            // GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, alphaColor, fadeDuration * Time.deltaTime);
            StartCoroutine(Fade());
            fading = true;
        }
    }

    private IEnumerator Fade()
    {
        for(float f = 1f; f >= -.20f; f -= 0.20f)
        {
            Color c = mr.material.color;
            c.a = f;
            mr.material.color = c;

            yield return new WaitForSeconds(0.05f);
            
        }
    }
}
