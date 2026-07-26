using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScallerUI : MonoBehaviour
{
    public Image BackgroundImage;
    public Slider slider;
    public TMP_Text sliderTitle;
    public TMP_Text amountText;
    // Start is called before the first frame update
    public void SetUp(float amount)
    {
        float sliderAmount = amount / (float)100;
        slider.value = sliderAmount;
        amountText.text = ((int)(amount)).ToString();
        //BackgroundImage.color = ColorConstantManager.GetImageDefualtColor();
    }
    public void addValue(float amount)
    {
        slider.value += amount;
        if (slider.value < 0) slider.value = 0;
        if (slider.value > 1) slider.value = 1;
    }
    public void OnValueChange()
    {
        amountText.text = ((int)(slider.value * 100)).ToString();
    }
}
