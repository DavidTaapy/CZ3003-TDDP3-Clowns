using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUIManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public string url_user;
    public GameObject[] inventorySlots;
    private int itemPerPage = 8;
    public int pageID;
    public Text pageText;
    public GameObject inventoryPanel;
    public GameObject itemPanel;
    public List<Item> inventory;
    private string userId = "7HHcjbfJq1kD8VFMHHDq";
    Sprite sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        // Need to get the correct userId from playfab
        var userDaoObject = GameObject.Find("UserDao").GetComponent<UserDao>();
        User user = userDaoObject.getUser(url_user, userId);
        inventory = user.getInventory();
        Debug.Log(inventory[0].ToJSON());

        //testing code
        //sprite = Resources.Load<Sprite> ("Sprites/Single Mode Scene Sprites");
        DisplayItems();
        inventoryPanel.gameObject.SetActive(true);
        itemPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdatePage();
    }
    
    void DisplayItems()
    {
        var tmp = inventory.Count;
        for (int i = 0; i < itemPerPage; i++)
        {
            if (i >= pageID * itemPerPage && i < (pageID + 1) * itemPerPage && tmp > 0)
            {
                //testing code
                sprite = Resources.Load<Sprite>(inventory[i].getSpriteSource());
                inventorySlots[i].gameObject.SetActive(true);
                inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
                inventorySlots[i].transform.GetChild(1).GetComponent<Text>().text = inventory[i].getItemCount().ToString();
                inventorySlots[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
                /*
                if (inventoryManager.items[i].itemCount == 0)
                {
                    inventorySlots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0);
                }
                else
                {
                    inventorySlots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255);
                }
                */
                tmp--;
            }
            else
            {
                inventorySlots[i].gameObject.SetActive(false);
            }
        }
        
        /*for (int j = tmp; j < itemPerPage; j++){
            inventorySlots[j].gameObject.SetActive(false);
        }*/
    }

    public void NextPage()
    {
        if (pageID >= Mathf.Floor((inventoryManager.items.Count - 1) / itemPerPage))
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
            pageID = Mathf.FloorToInt((inventoryManager.items.Count - 1) / itemPerPage);
        }
        else
        {
            pageID--;
        }

        DisplayItems();
    }

    void UpdatePage()
    {
        pageText.text = (pageID + 1) + "/" + (Mathf.FloorToInt((inventorySlots.Length - 1) / itemPerPage) + 1).ToString();
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

    void UpdateItemPanel(GameObject currentItemButton)
    {
        GameObject currentItem = currentItemButton.transform.parent.gameObject;
        int currentItemIndex = currentItem.transform.GetSiblingIndex();

        itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = inventoryManager.items[currentItemIndex].itemSprite;
        itemPanel.transform.GetChild(1).GetComponent<Text>().text = "ID: " + inventoryManager.items[currentItemIndex].itemID.ToString();
        itemPanel.transform.GetChild(2).GetComponent<Text>().text = "Name: " + inventoryManager.items[currentItemIndex].itemName;
        itemPanel.transform.GetChild(3).GetComponent<Text>().text = "Price: " + inventoryManager.items[currentItemIndex].price.ToString();
        itemPanel.transform.GetChild(5).GetComponent<Text>().text = "You have: " + inventoryManager.items[currentItemIndex].itemCount.ToString();
        itemPanel.transform.GetChild(6).GetComponent<Text>().text = inventoryManager.items[currentItemIndex].itemDescription.ToString();

        switch (inventoryManager.items[currentItemIndex].itemSource)
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
        /*
        if (inventoryManager.items[currentItemIndex].itemCount == 0)
        {
            itemPanel.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0);
        }
        else
        {
            itemPanel.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255);
        }
        */
    }
}
