using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUIManager : MonoBehaviour
{
    private string url_user = "http://localhost:3000/user";
    private string userId = "";

    [Header("Inventory Details")]
    public GameObject inventoryPanel;
    public GameObject[] inventorySlots;
    private List<Item> inventory;

    [Header("Page Details")]
    private int itemPerPage = 8;
    private int pageID;
    public Text pageText;

    [Header("View Item Details")]
    public GameObject itemPanel;
    Sprite sprite;
    
    void Start()
    {
        // Need to get the correct userId from playfab
        userId = PlayerPrefs.GetString("uid");
        var userDaoObject = GameObject.Find("UserDao").GetComponent<UserDao>();
        User user = userDaoObject.getUser(url_user, userId);
        inventory = user.getInventory();

        DisplayItems();
        inventoryPanel.gameObject.SetActive(true);
        itemPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdatePage();
    }
    
    private void DisplayItems()
    {
        int tmp;

        if (inventory.Count < itemPerPage)
        {
            tmp = inventory.Count; 
        }
        else
        {
            if (pageID == 0)
            {
                tmp = itemPerPage;
            }
            else
            {
                tmp = inventory.Count - (pageID * itemPerPage);
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i >= pageID * itemPerPage && i < (pageID + 1) * itemPerPage && tmp > 0)
            {
                sprite = Resources.Load<Sprite>(inventory[i].getSpriteSource());
                inventorySlots[i].gameObject.SetActive(true);
                inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                inventorySlots[i].transform.GetChild(1).GetComponent<Text>().text = inventory[i].getItemCount().ToString();
                inventorySlots[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
                tmp--;
            }
            else
            {
                inventorySlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void NextPage()
    {
        if (pageID >= Mathf.Floor((inventory.Count - 1) / itemPerPage))
        {
            pageID = 0;
        }
        else
        {
            pageID++;
        }

        DisplayItems();
    }

    public void BackPage()
    {
        if (pageID <= 0)
        {
            pageID = Mathf.FloorToInt((inventory.Count - 1) / itemPerPage);
        }
        else
        {
            pageID--;
        }

        DisplayItems();
    }

    private void UpdatePage()
    {
        pageText.text = (pageID + 1) + "/" + (Mathf.Ceil((inventorySlots.Length) / itemPerPage)).ToString();
    }

    public void ActivateItemPanel()
    {
        GameObject currentItemButton = EventSystem.current.currentSelectedGameObject;
        UpdateItemPanel(currentItemButton);
        inventoryPanel.gameObject.SetActive(false);
        itemPanel.gameObject.SetActive(true);
    }

    public void ActivateInventoryPanel()
    {
        inventoryPanel.gameObject.SetActive(true);
        itemPanel.gameObject.SetActive(false);
    }

    private void UpdateItemPanel(GameObject currentItemButton)
    {
        GameObject currentItem = currentItemButton.transform.parent.gameObject;
        int currentItemIndex = currentItem.transform.GetSiblingIndex();

        sprite = Resources.Load<Sprite>(inventory[currentItemIndex].getSpriteSource());
        itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        itemPanel.transform.GetChild(2).GetComponent<Text>().text = "Name: " + inventory[currentItemIndex].getItemName();
        itemPanel.transform.GetChild(3).GetComponent<Text>().text = "Price: " + inventory[currentItemIndex].getPrice().ToString();
        itemPanel.transform.GetChild(5).GetComponent<Text>().text = "You have: " + inventory[currentItemIndex].getItemCount().ToString();
        itemPanel.transform.GetChild(6).GetComponent<Text>().text = inventory[currentItemIndex].getItemDescription().ToString();

        switch (inventory[currentItemIndex].itemSource)
        {
            case ItemSource.Shop:
            {
                itemPanel.transform.GetChild(4).GetComponent<Text>().text = "Source: Shop";
                break;
            }
            case ItemSource.Character:
            {
                itemPanel.transform.GetChild(4).GetComponent<Text>().text = "Source: Character";
                break;
            }
            case ItemSource.Leaderboard:
            {
                itemPanel.transform.GetChild(4).GetComponent<Text>().text = "Source: Leaderboard";
                break;
            }
        }
    }
}