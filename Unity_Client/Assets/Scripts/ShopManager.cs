using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    private string userId = "7HHcjbfJq1kD8VFMHHDq";
    private string url_user = "http://localhost:3000/user";
    private string url_items = "http://localhost:3000/items";

    [Header("User Details")]
    public User user;
    private int userPoints;

    [Header("Powerup Details")]
    public GameObject powerupPanel;
    public GameObject[] powerupSlots;
    public List<Item> shopPowerups;

    [Header("Accessory Details")]
    public GameObject accessoryPanel;
    public GameObject[] accessorySlots;
    public List<Item> shopAccessory;

    [Header("View Item Details")]
    public GameObject itemPanel;
    private GameObject currentItem;
    
    [Header("Page Details")]
    private int itemPerPage = 8;
    public Text pointsText;
    public Text pageText;
    public GameObject toAccessoryPage;
    public GameObject toPowerupPage;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise panels and text
        pageText.text = "Powerups";
        itemPanel.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(false);
        toPowerupPage.gameObject.SetActive(false);
        
        // Initialise variables with backend
        user = getUserDetails(url_user, userId);
        shopPowerups = getShopPowerups(url_items);
        shopAccessory = getShopAccessory(url_items);
        userPoints = user.getPoints();
        pointsText.text = "Points: " + userPoints.ToString();

        // Initialise powerups
        displayPowerups();
    }

    private User getUserDetails(string url_user, string userId){
        var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        User user = linktoUserGet.getUser(url_user, userId);
        return user;
    }

    private List<Item> getShopPowerups(string url){
        var linktoItems = GameObject.Find("ItemsDao").GetComponent<ItemDao>();
        List<Item> shopPowerUps = linktoItems.getItems(url_items, "Powerup", "Shop"); //returns list of powerups in shop
        Debug.Log("\n num of shop powerup: " + shopPowerUps.Count);
        foreach (Item i in shopPowerUps)
        {
            Debug.Log(i.ToJSON());
        }
        return shopPowerUps;
    }

    private List<Item> getShopAccessory(string url){
        var linktoItems = GameObject.Find("ItemsDao").GetComponent<ItemDao>();
        List<Item> shopAccessory = linktoItems.getItems(url_items, "Skin", "Shop"); //returns list of powerups in shop
        Debug.Log("\n num of shop skins: " + shopAccessory.Count);
        foreach (Item i in shopAccessory)
        {
            Debug.Log(i.ToJSON());
        }
        return shopAccessory;
    }

    public void activatePowerupPanel()
    {  
        pageText.text = "Powerups";

        itemPanel.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(false);
        toPowerupPage.gameObject.SetActive(false);
        powerupPanel.gameObject.SetActive(true);
        toAccessoryPage.gameObject.SetActive(true);
        
        displayPowerups();
    }

    public void activateAccessoryPanel()
    {
        pageText.text = "Accessories";

        itemPanel.gameObject.SetActive(false);
        powerupPanel.gameObject.SetActive(false);
        toAccessoryPage.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(true);
        toPowerupPage.gameObject.SetActive(true);

        displayAccessory();
    }

    public void activateItemPanel()
    {
        // GameObject currentItemButton = EventSystem.current.currentSelectedGameObject;
        currentItem = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        updateItemPanel(currentItem);

        powerupPanel.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(false);
        toPowerupPage.gameObject.SetActive(false);
        toAccessoryPage.gameObject.SetActive(false);
        itemPanel.gameObject.SetActive(true);
    }

    void displayPowerups()
    {
        int tmp = shopPowerups.Count;
        for (int i = 0; i < itemPerPage; i++)
        {
            if (tmp > 0)
            {
                powerupSlots[i].gameObject.SetActive(true);
                powerupSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = shopPowerups[i].getItemSprite();
                powerupSlots[i].transform.GetChild(1).GetComponent<Text>().text = shopPowerups[i].getPrice().ToString();
                powerupSlots[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
                tmp--;
            }
            else
            {
                powerupSlots[i].gameObject.SetActive(false);
            }
        }
    }

    void displayAccessory()
    {
        int tmp = shopAccessory.Count;
        for (int i = 0; i < itemPerPage; i++)
        {
            if (tmp > 0)
            {
                accessorySlots[i].gameObject.SetActive(true);
                accessorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = shopAccessory[i].getItemSprite();
                accessorySlots[i].transform.GetChild(1).GetComponent<Text>().text = shopAccessory[i].getPrice().ToString();
                accessorySlots[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
                tmp--;
            }
            else
            {
                accessorySlots[i].gameObject.SetActive(false);
            }
        }
    }

    void updateItemPanel(GameObject currentItem)
    {
        int currentItemIndex = currentItem.transform.GetSiblingIndex();

        if (currentItem.transform.parent.gameObject == powerupPanel)
        {
            itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = shopPowerups[currentItemIndex].getItemSprite();
            itemPanel.transform.GetChild(1).GetComponent<Text>().text = "Name: " + shopPowerups[currentItemIndex].getItemName();
            itemPanel.transform.GetChild(2).GetComponent<Text>().text = "Price: " + shopPowerups[currentItemIndex].getPrice().ToString();
            itemPanel.transform.GetChild(3).GetComponent<Text>().text = shopPowerups[currentItemIndex].getItemDescription().ToString();
        }
        else
        {
            itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = shopAccessory[currentItemIndex].getItemSprite();
            itemPanel.transform.GetChild(1).GetComponent<Text>().text = "Name: " + shopAccessory[currentItemIndex].getItemName();
            itemPanel.transform.GetChild(2).GetComponent<Text>().text = "Price: " + shopAccessory[currentItemIndex].getPrice().ToString();
            itemPanel.transform.GetChild(3).GetComponent<Text>().text = shopAccessory[currentItemIndex].getItemDescription().ToString();
        }
    }

    // TODO: buy item and update user inventory, substract userpoints by price
    public void purchaseItem()
    {
        int currentItemIndex = currentItem.transform.GetSiblingIndex();
        int price = shopPowerups[currentItemIndex].getPrice();

        if (userPoints >= price)
        {
            userPoints -= price;
            print("Purchase successful!");
        }
        else
        {
            print("Not enough points!");
        }
    }
}
