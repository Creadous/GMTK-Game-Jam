using UnityEngine;
using TMPro;

public class CantAffordMessage : MonoBehaviour
{
    public GameObject messagePanel;
    public TMP_Text messageText;
    private bool isShowing = false;

    public void ShowMessage()
    {
        Debug.Log("ShowMessage called, isShowing: " + isShowing);
        if (isShowing) return;
        isShowing = true;
        messagePanel.SetActive(true);
        messageText.text = "Not with the gold you have friend";
        Invoke(nameof(HideMessage), 3f);
    }

    private void HideMessage()
    {
        messagePanel.SetActive(false);
        isShowing = false;
    }
}