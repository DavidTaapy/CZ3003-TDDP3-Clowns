using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    private string url_items;
    private string url_user;

    [Header("User Details")]
    public string userId = "7HHcjbfJq1kD8VFMHHDq";
    public User user;
    private int userPoints;
    private List<Item> userInventory;

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
    
    [Header("Page Details")]
    private int itemPerPage = 8;
    public Text pointsText;
    public Text pageText;
    public GameObject toAccessoryPage;
    public GameObject toPowerupPage;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise panels
        itemPanel.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(false);
        toPowerupPage.gameObject.SetActive(false);
        
        // Initialise variables with backend
        user = getUserDetails(url_user, userId);
        shopPowerups = getShopPowerups(url_items);
        shopAccessory = getShopAccessory(url_items);
        userPoints = user.getPoints();
        userInventory = user.getInventory();

        // Initialise text
        pageText.text = "Powerups";
        pointsText.text = userPoints.ToString();

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
        GameObject currentItemButton = EventSystem.current.currentSelectedGameObject;
        updateItemPanel(currentItemButton);

        powerupPanel.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(false);
        toPowerupPage.gameObject.SetActive(false);
        toAccessoryPage.gameObject.SetActive(false);
        itemPanel.gameObject.SetActive(true);
    }

    void displayPowerups()
    {
        for (int i = 0; i < itemPerPage; i++)
        {
            powerupSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = shopPowerups[i].itemSprite;
            powerupSlots[i].transform.GetChild(1).GetComponent<Text>().text = shopPowerups[i].price.ToString();
            powerupSlots[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void displayAccessory()
    {
        for (int i = 0; i < itemPerPage; i++)
        {
            accessorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = shopAccessory[i].itemSprite;
            accessorySlots[i].transform.GetChild(1).GetComponent<Text>().text = shopAccessory[i].price.ToString();
            accessorySlots[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void updateItemPanel(GameObject currentItemButton)
    {
        GameObject currentItem = currentItemButton.transform.parent.gameObject;
        int currentItemIndex = currentItem.transform.GetSiblingIndex();

        if (currentItem.transform.parent.gameObject == powerupPanel)
        {
            itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = shopPowerups[currentItemIndex].itemSprite;
            itemPanel.transform.GetChild(1).GetComponent<Text>().text = "Name: " + shopPowerups[currentItemIndex].itemName;
            itemPanel.transform.GetChild(2).GetComponent<Text>().text = "Price: " + shopPowerups[currentItemIndex].price.ToString();
            itemPanel.transform.GetChild(3).GetComponent<Text>().text = shopPowerups[currentItemIndex].itemDescription.ToString();
        }
        else
        {
            itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = shopAccessory[currentItemIndex].itemSprite;
            itemPanel.transform.GetChild(1).GetComponent<Text>().text = "Name: " + shopAccessory[currentItemIndex].itemName;
            itemPanel.transform.GetChild(2).GetComponent<Text>().text = "Price: " + shopAccessory[currentItemIndex].price.ToString();
            itemPanel.transform.GetChild(3).GetComponent<Text>().text = shopAccessory[currentItemIndex].itemDescription.ToString();
        }
    }

    // TODO: buy item and update user inventory, substract userpoints by price
    public void purchaseItem(int price)
    {
        if (userPoints >= price)
        {
            UpdatePoints(price);
            Debug.Log("Purchase successful!");
        }
        else
        {
            Debug.Log("Not enough points!");
        }
    }

    void UpdatePoints(int price)
    {
        userPoints -= price;
    }
}
