using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopularityBar : MonoBehaviour
{
    private bool isPreviewing = false;
    private float sliderValue = 0f;
    public Slider slider;
    public Image sliderHandle;

    public void SetValue(float value, bool isPreview)
    {
        sliderValue = value;
        isPreviewing = isPreview;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SliderAnimation());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SliderAnimation()
    {
        while (true)
        {
            slider.value = Mathf.Lerp(slider.value, sliderValue, 3 * Time.deltaTime);
            if (isPreviewing)
            {
                sliderHandle.color = new Color(1, 1, 1, 0.5f + 0.2f * (float)Math.Sin(5 * Time.time));
            }
            else
            {
                sliderHandle.color = new Color(1, 1, 1);
            }
            yield return null;
        }
    }
}
