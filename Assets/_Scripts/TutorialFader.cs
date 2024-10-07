using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFader : MonoBehaviour
{    
    private Image tutorialImage;

    private bool tutorialCleared = false;

    private void Start()
    {
        this.tutorialImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && this.tutorialCleared == false)
        {
            this.tutorialCleared = true;

            StartCoroutine(this.FadeImage());
        }
    }

    private IEnumerator FadeImage()
    {
        yield return new WaitForSeconds(1.0f);

        while (this.tutorialImage.color.a > 0.0f)
        {
            float newAlpha = this.tutorialImage.color.a - 0.02f;
            Color updatedColor = new Color(this.tutorialImage.color.r, this.tutorialImage.color.g, this.tutorialImage.color.b, newAlpha);
            this.tutorialImage.color = updatedColor;

            yield return new WaitForFixedUpdate();
        }
    }
}
