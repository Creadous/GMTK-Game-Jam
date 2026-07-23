using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BossRoomCountDownHud : MonoBehaviour
{
    public TMP_Text bossCountDownText;
    public void UpateCountDown(int amount)
    {
        bossCountDownText.text = amount.ToString();
        if(amount == 0)
        {
            this.gameObject.SetActive(false); //hide the hud because your here
        }
    }
}
