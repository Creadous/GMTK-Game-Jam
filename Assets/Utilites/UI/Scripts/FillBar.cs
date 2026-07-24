using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FillBar : MonoBehaviour
{
    public Image fillAmount;
    public TMP_Text fillAmountText;

    public void UpdateFillBar(int currentAmount, int maxAmount)
    {
        float fillAmountFloat = currentAmount / (float)maxAmount;
        fillAmount.fillAmount = fillAmountFloat;
        fillAmountText.text = currentAmount.ToString();
    }
}
