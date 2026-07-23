using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardScrollViewLogic : MonoBehaviour
{
    public bool disabledForLoading;
    [HideInInspector] public ButtonSelectionBase buttonSelectionBase;

    [Header("Scrollview")]
    public float scrollSpeed = 10f;
    public ScrollRect scrollRect;
    public RectTransform scrollviewport;
    public RectTransform scrollContent;
    private RectTransform m_SelectedRectTransform;

    private void Awake()
    {
       if(buttonSelectionBase == null) buttonSelectionBase = GetComponent<ButtonSelectionBase>();
    }

    private void Update()
    {
        if (buttonSelectionBase.buttonBaseList == null || buttonSelectionBase.buttonBaseList.Count <= 0  || disabledForLoading) return; //no list

        var slot = buttonSelectionBase.buttonBaseList[buttonSelectionBase.selectedIndex];
        UtilitesHelper.UpdateScrollToSelected(slot.gameObject, scrollSpeed, scrollRect, scrollContent, scrollviewport);
    }
}
