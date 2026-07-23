using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilitesHelper
{
    public static int CycleNumber(int currentValue, int totalAmount, int cycleAmount)
    {
        currentValue = currentValue + cycleAmount;
        if (currentValue < 0) currentValue = totalAmount - 1;
        if (currentValue >= totalAmount) currentValue = 0;
        return currentValue;
    }
    public static void UpdateScrollToSelected(GameObject selectedObject, float scrollSpeed, ScrollRect scrollRect, RectTransform contentRectTransform, RectTransform viewportRectTransform)
    {
        if (selectedObject == null)
        {
            return;
        }
        if (selectedObject.transform.parent != contentRectTransform.transform)
        {
            return;
        }

        RectTransform m_SelectedRectTransform = selectedObject.GetComponent<RectTransform>();

        // Math stuff
        Vector3 selectedDifference = viewportRectTransform.localPosition - m_SelectedRectTransform.localPosition;
        float contentHeightDifference = (contentRectTransform.rect.height - viewportRectTransform.rect.height);

        float selectedPosition = (contentRectTransform.rect.height - selectedDifference.y);
        float currentScrollRectPosition = scrollRect.normalizedPosition.y * contentHeightDifference;
        float above = currentScrollRectPosition - (m_SelectedRectTransform.rect.height / 2) + viewportRectTransform.rect.height / 2;
        float below = currentScrollRectPosition + (m_SelectedRectTransform.rect.height / 2) - viewportRectTransform.rect.height / 2;

        //Debug.Log("scrollLogic above:" + above + " below: " + below + " selectPostion: " + selectedPosition);

        // Check if selected option is out of bounds.
        if (selectedPosition > above)
        {
            float step = selectedPosition - above;
            float newY = currentScrollRectPosition + step;
            float newNormalizedY = newY / contentHeightDifference;
            scrollRect.normalizedPosition = Vector2.Lerp(scrollRect.normalizedPosition, new Vector2(0, newNormalizedY), scrollSpeed * Time.deltaTime);
        }
        else if (selectedPosition < below)
        {
            float step = selectedPosition - below;
            float newY = currentScrollRectPosition + step;
            float newNormalizedY = newY / contentHeightDifference;
            scrollRect.normalizedPosition = Vector2.Lerp(scrollRect.normalizedPosition, new Vector2(0, newNormalizedY), scrollSpeed * Time.deltaTime);
        }
    }
    /*
    #region item add and removing

    public static List<Item> AddItemsToItemList(List<Item> itemList, Item newItem)
    {
        //add gold
        if (newItem.itemStats.itemType == ItemType.Gold)
        {
            AdventureSaveData.instance.UpdateGoldAmount(newItem.amountInStack);
            return itemList;
        }

        //check if the gathering resource amount need to be update by item addition
        GatherResourceTracker.instance.CheckNewItem(newItem.itemStats);
        

        Item itemInInventory = UtilitesHelper.GetItemFromList(itemList, newItem.itemStats.itemID);
        if (itemInInventory == null)
        {
            itemList.Add(newItem);
        }
        else
        {
            if (newItem.itemStats.stackable == true)
            {
                itemInInventory.amountInStack += newItem.amountInStack;
            }
            else
            {
                itemList.Add(newItem);
            }
        }

        return itemList;
    }

    public static Item GetItemFromList(List<Item> itemList, string itemkey)
    {
        return itemList.Find(x => x.itemStats.itemID == itemkey);
    }
    public static List<Item> RemoveItemFromItemList(List<Item> itemList, Item newItem)
    {
        Item itemInInventory = UtilitesHelper.GetItemFromList(itemList, newItem.itemStats.itemID);
        if (itemInInventory.itemStats.stackable == true)
        {
            if (itemInInventory.amountInStack == newItem.amountInStack)
            {
                Item inStock = itemList.Find(x => x.itemStats.itemID == newItem.itemStats.itemID);
                itemList.Remove(inStock);
            }
            else
            {
                itemInInventory.amountInStack = itemInInventory.amountInStack - newItem.amountInStack;
            }
        }
        else
        {
            //Not we might need to add other condution like skill attacked and weather its equiped
            for (int i = 0; i < itemList.Count; i++)
            {
                if (newItem.IsEqual(itemList[i]))
                {
                    Item inStock = itemList[i];
                    itemList.Remove(inStock);
                    break; // you found it great!
                }
            }
        }

        return itemList;
    }
    #endregion
    */
}
