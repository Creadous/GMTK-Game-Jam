using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MenuButtonBase : MonoBehaviour
{
    [Header("MenuButtonBase")]
    [SerializeField] private GameObject backgroundImage;
    [SerializeField] private GameObject selectedImage;
    [SerializeField] protected TMP_Text text;
    [SerializeField] private string stringKey;

    public void Awake()
    {
    }

    public virtual void ToggleSelect(bool toggle)
    {
        if(toggle == true)
        {
            if(backgroundImage != null) backgroundImage.SetActive(false);
            if(selectedImage != null) selectedImage.SetActive(true);
        }
        else
        {
            if(backgroundImage != null) backgroundImage.SetActive(true);
            if(selectedImage != null) selectedImage.SetActive(false);
        }
    }
    public string GetStringKey()
    {
        return stringKey;
    }
}
