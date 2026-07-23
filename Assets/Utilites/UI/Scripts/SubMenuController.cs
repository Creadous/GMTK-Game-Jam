using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
public class SubMenuController : MonoBehaviour
{
    [Header("SubMenuController")]
    [Header("UI Elements")]
    public Transform container;
    public TMP_Text menuTitleText;
    public CanvasGroup canvasGroup;
    public Transform ghostMenuContainer; // used to fill out menu slots but not actaully be selecteable
    public ButtonSelectionBase ghostButtons;
    [HideInInspector] protected ButtonSelectionBase buttonSelectionBase;

   // public Image ScrollBackground;

    [Header("Slots")]
    public GameObject slotPrefab;
    public List<MenuSlot> SlotList;
    //public GameObject upgradeSlotPrefab;
    //public List<UpgradeSlot> UpgradeSlotList;


    [Header("Menu setting")]
    public int slotMiniSize = 30;
   
    public UnityEvent SelectionAcceptedCallback;
    public UnityEvent SelectionChangedCallback;

    private void Awake()
    {
        buttonSelectionBase = GetComponent<ButtonSelectionBase>();
        //Color colorBG = ColorConstantManager.GetComplentySplit(true);
        //ScrollBackground.color = new Color(colorBG.a, colorBG.g, colorBG.b,0.5f);
    }
    public void Start()
    {
        if (buttonSelectionBase == null) return; //TEMP to get the thing running
        buttonSelectionBase.SelectionAcceptedCallback.AddListener(() => SelectionAcceptedCallback.Invoke());
        buttonSelectionBase.SelectionChangedCallback.AddListener(() => SelectionChangedCallback.Invoke());
    }
    public void OnDestroy()
    {
        SelectionAcceptedCallback.RemoveAllListeners();
        SelectionChangedCallback.RemoveAllListeners();
    }
    /*
    public void PopulateItemListCombat(List<Item> items)
    {
        StartPopulationMenu("Item");
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemStats.itemType == ItemType.UseableItem)
            {
                ItemStatsUsable itemStatsUsable = (ItemStatsUsable)items[i].itemStats;
                if (itemStatsUsable.useInCombat)
                {
                    SetUpSlot(Inventory.instance.inventoryList[i], SlotUIType.ItemCombat);
                }
            }
        }
        FinishedPopulationMenu();
    }
    public void PopulateItemList(List<Item> items, List<ItemType> requirments)
    {
        StartPopulationMenu("Item");
        for (int i = 0; i < items.Count; i++)
        {
            if (requirments.Contains(items[i].itemStats.itemType))
            {
                SetUpSlot(Inventory.instance.inventoryList[i], SlotUIType.ItemNormal);
            }
        }
        FinishedPopulationMenu();
    }
    public void PopulateSkillList(CombatStats combatStats)
    {
        StartPopulationMenu("Skills");
        if (combatStats.skills == null) return;
        for (int i = 0; i < combatStats.skills.Count; i++)
        {
            SetUpSlot(combatStats.skills[i], SlotUIType.SkillCombat);
        }

        FinishedPopulationMenu();
    }

    public void PopulateLimitList(CombatStats combatStats)
    {
        StartPopulationMenu("Limit break");
        if (combatStats.limitBreakSkill == null) return;
        for (int i = 0; i < combatStats.limitBreakSkill.Count; i++)
        {
            SetUpSlot(combatStats.limitBreakSkill[i], SlotUIType.LimitSkillCombat);
        }

        FinishedPopulationMenu();
    }
    */
    protected void StartPopulationMenu(string titleText)
    {
        DisableKeyboardInput(true);
        if (SlotList == null) SlotList = new List<MenuSlot>();
        ClearMenu();
        menuTitleText.text = titleText;
    }
    protected void FinishedPopulationMenu()
    {
        buttonSelectionBase.BuildButtonList();
        SelectionChangedCallback.Invoke(); //tell everyone listen to updated
        if(this.gameObject.activeInHierarchy == true)
        {
            StartCoroutine(KeyboardWaitTime());
        }
        else
        {
            //if not visable unlock right away
            DisableKeyboardInput(false);
        }
    }

    //this is here to fix a scrollview bug where if you load to slowly the keyboardscrollviewlogic blows up and give you garabage data. DONT REMOVE THIS
    private IEnumerator KeyboardWaitTime()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        DisableKeyboardInput(false);
    }

    //used to fix infinite scrollbar bug
    public void DisableKeyboardInput(bool value)
    {
        GetComponent<KeyBoardScrollViewLogic>().disabledForLoading = value;
    }

    /*
    public void PopulateCharacters(string menuTitle, List<CharacterData> characters, bool displayPartyStatus)
    {
        if (SlotList == null) SlotList = new List<MenuSlot>();
        ClearMenu();
        menuTitleText.text = menuTitle;
        int currentNumber = 0;
        for(int i = 0; i < characters.Count; i++)
        {
            if (displayPartyStatus)
            {
                SetUpSlot(characters[i], SlotUIType.CharacterPartyStatus);
            }
            else
            {
                SetUpSlot(characters[i], SlotUIType.Character);
            }
            currentNumber++;
        }


        buttonSelectionBase.BuildButtonList();
        SelectionChangedCallback.Invoke(); //tell everyone listen to updated
    }
    */

    public void PopulateList(string menuTitle, List<object> objectList, SlotUIType slotUIType)
    {
        StartPopulationMenu(menuTitle);

        for (int i = 0; i < objectList.Count; i++)
        {
            SetUpSlot(objectList[i], slotUIType);
        }

        FinishedPopulationMenu();
    }
   
    //helper funtion
    protected MenuSlot SetUpSlot(object refItem, SlotUIType slotUIType) 
    {
        MenuSlot slot = Instantiate(slotPrefab, container).GetComponent<MenuSlot>();
        slot.SetUp(refItem, slotUIType, slotMiniSize);
        SlotList.Add(slot);
        buttonSelectionBase.buttonBaseList.Add(slot);
        return slot;
    }

    public void SetSelectionVisiblity(int index, bool visiable)
    {
        buttonSelectionBase.SetSelectionVisiblity(index, visiable);
    }

    public int GetMenuSlotCount()
    {
        return SlotList.Count;
    }
    public void ClearMenu()
    {
        //clearing
        for (int i = SlotList.Count -1; i >-1;  i--)
        {
            GameObject.Destroy(SlotList[i].gameObject);
        }
        SlotList.Clear();
        if (buttonSelectionBase == null) buttonSelectionBase = GetComponent<ButtonSelectionBase>();
        buttonSelectionBase.ClearList();
    }
   
    public MenuSlot GetSelectSlot()
    {
        if (SlotList.Count <= 0) return null;
        return SlotList[buttonSelectionBase.selectedIndex];
    }
    public MenuSlot GetSelectSlot(int index)
    {
        if (SlotList.Count <= 0) return null;
        return SlotList[index];
    }
    public void HandleInput()
    {
        buttonSelectionBase.HandleButtonCycle(InputManager.instance.move.y);
        buttonSelectionBase.HandleButtonInputs();
    }
    /*
    #region UpgradeSlot logic

    public void PopulateSkillList(string menuTitle, List<SkillData> skills, bool levelUp, bool hideLevel, int fontSize)
    {
        if (SlotList == null) UpgradeSlotList = new List<UpgradeSlot>();

        //clearing
        for (int i = 0; i < UpgradeSlotList.Count; i++)
        {
            Destroy(UpgradeSlotList[i].gameObject);
        }
        UpgradeSlotList.Clear();
        if (buttonSelectionBase == null) buttonSelectionBase = GetComponent<ButtonSelectionBase>();
        buttonSelectionBase.ClearList();


        menuTitleText.text = menuTitle;

        for (int i = 0; i < skills.Count; i++)
        {
            SetUpUpgradeSlot(skills[i], levelUp, hideLevel, fontSize);
        }
        buttonSelectionBase.BuildButtonList();

        SelectionChangedCallback.Invoke(); //tell everyone listen to updated
    }

    private void SetUpUpgradeSlot(object refItem, bool levelUp, bool hideLevel, int fontSize)
    {
        UpgradeSlot slot = Instantiate(upgradeSlotPrefab, container).GetComponent<UpgradeSlot>();
        slot.Setup((SkillData)refItem, levelUp, fontSize);
        if (hideLevel) slot.HideLevel();
        UpgradeSlotList.Add(slot);
        buttonSelectionBase.buttons.Add(slot.gameObject);
    }

    public UpgradeSlot GetSelectedUpgradeSlot()
    {
        if (UpgradeSlotList.Count <= 0) return null;
        return UpgradeSlotList[buttonSelectionBase.selectedIndex];
    }


    #endregion
    */
    #region Helpers
    public void ResetMenuLocation()
    {
        if (buttonSelectionBase == null) buttonSelectionBase = GetComponent<ButtonSelectionBase>();
        buttonSelectionBase.Restart(); //reset
    }
    public int GetSelectIndex()
    {
        return buttonSelectionBase.selectedIndex;
    }
    //helper when there is more then 1 menu open
    public void FocusOn(bool value)
    {
        if (value)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 0.5f;
        }
    }
    public bool AbleToAct()
    {
        if (buttonSelectionBase.currentDelay < 0)
        {
            return true;
        }
        return false;
    }
    public void Act()
    {
        buttonSelectionBase.currentDelay = buttonSelectionBase.delayAmount;
    }
    #endregion
}
