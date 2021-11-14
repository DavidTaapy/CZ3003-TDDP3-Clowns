using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManager : MonoBehaviour
{
    private string url_user = "http://localhost:3000/user";
    private string url_items = "http://localhost:3000/items";
    string userId = "";

    [Header("User Details")]
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
    private GameObject currentItem;
    public Text pointsText;
    
    [Header("Page Details")]
    private int itemPerPage = 8;
    public Text pageText;
    public GameObject toAccessoryPage;
    public GameObject toPowerupPage;
    Sprite sprite;

    public TMP_Text warningRegisterText;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise panels and text
        warningRegisterText.gameObject.SetActive(false);
        userId = PlayerPrefs.GetString("uid");
        pageText.text = "Powerups";
        itemPanel.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(false);
        toPowerupPage.gameObject.SetActive(false);
        
        // Initialise variables with backend
        user = GetUserDetails(url_user, userId);
        shopPowerups = GetShopPowerups(url_items);
        shopAccessory = GetShopAccessory(url_items);
        userPoints = user.getPoints();
        pointsText.text = "Points: " + userPoints.ToString();
        userInventory = user.getInventory();

        // Initialise powerups
        DisplayPowerups();
    }

    private User GetUserDetails(string url_user, string userId){
        var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        User user = linktoUserGet.getUser(url_user, userId);
        return user;
    }

    private List<Item> GetShopPowerups(string url){
        var linktoItems = GameObject.Find("ItemsDao").GetComponent<ItemDao>();
        List<Item> shopPowerUps = linktoItems.getItems(url_items, "Powerup", "Shop"); //returns list of powerups in shop
        return shopPowerUps;
    }

    private List<Item> GetShopAccessory(string url){
        var linktoItems = GameObject.Find("ItemsDao").GetComponent<ItemDao>();
        List<Item> shopAccessory = linktoItems.getItems(url_items, "Accessory", "Shop"); //returns list of accessory in shop
        return shopAccessory;
    }

    private void UpdateUserInventory(string url, User user) {
        var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        linktoUserGet.updateUser(url, user);
    }

    public void ActivatePowerupPanel()
    {  
        pageText.text = "Powerups";

        itemPanel.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(false);
        toPowerupPage.gameObject.SetActive(false);
        powerupPanel.gameObject.SetActive(true);
        toAccessoryPage.gameObject.SetActive(true);

        DisplayPowerups();
    }

    public void ActivateAccessoryPanel()
    {
        pageText.text = "Accessories";

        itemPanel.gameObject.SetActive(false);
        powerupPanel.gameObject.SetActive(false);
        toAccessoryPage.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(true);
        toPowerupPage.gameObject.SetActive(true);

        DisplayAccessory();
    }

    public void ActivateItemPanel()
    {
        currentItem = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        UpdateItemPanel(currentItem);

        powerupPanel.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(false);
        toPowerupPage.gameObject.SetActive(false);
        toAccessoryPage.gameObject.SetActive(false);
        itemPanel.gameObject.SetActive(true);
    }

    private void DisplayPowerups()
    {
        int tmp = shopPowerups.Count;
        for (int i = 0; i < itemPerPage; i++)
        {
            if (tmp > 0)
            {
                sprite = Resources.Load<Sprite>(shopPowerups[i].getSpriteSource());
                powerupSlots[i].gameObject.SetActive(true);
                powerupSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
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

    private void DisplayAccessory()
    {
        int tmp = shopAccessory.Count;
        for (int i = 0; i < itemPerPage; i++)
        {
            if (tmp > 0)
            {
                sprite = Resources.Load<Sprite>(shopAccessory[i].getSpriteSource());
                accessorySlots[i].gameObject.SetActive(true);
                accessorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
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

    private void UpdateItemPanel(GameObject currentItem)
    {
        int currentItemIndex = currentItem.transform.GetSiblingIndex();
        GameObject currentPanel = currentItem.transform.parent.gameObject;
        warningRegisterText.gameObject.SetActive(false);

        if (currentPanel == powerupPanel)
        {
            sprite = Resources.Load<Sprite>(shopPowerups[currentItemIndex].getSpriteSource());
            itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            itemPanel.transform.GetChild(1).GetComponent<Text>().text = "Name: " + shopPowerups[currentItemIndex].getItemName();
            itemPanel.transform.GetChild(2).GetComponent<Text>().text = "Price: " + shopPowerups[currentItemIndex].getPrice().ToString();
            itemPanel.transform.GetChild(3).GetComponent<Text>().text = shopPowerups[currentItemIndex].getItemDescription().ToString();
            itemPanel.transform.GetChild(4).GetComponent<Text>().text = shopPowerups[currentItemIndex].getItemID().ToString();
        }
        else
        {
            sprite = Resources.Load<Sprite>(shopAccessory[currentItemIndex].getSpriteSource());
            itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            itemPanel.transform.GetChild(1).GetComponent<Text>().text = "Name: " + shopAccessory[currentItemIndex].getItemName();
            itemPanel.transform.GetChild(2).GetComponent<Text>().text = "Price: " + shopAccessory[currentItemIndex].getPrice().ToString();
            itemPanel.transform.GetChild(3).GetComponent<Text>().text = shopAccessory[currentItemIndex].getItemDescription().ToString();
            itemPanel.transform.GetChild(4).GetComponent<Text>().text = shopAccessory[currentItemIndex].getItemID().ToString();
        }

        itemPanel.transform.GetChild(4).gameObject.SetActive(false);
    }

    public void PurchaseItem()
    {
        int price;
        Item purchasedItem;
        int currentItemIndex = currentItem.transform.GetSiblingIndex();
        GameObject currentPanel = currentItem.transform.parent.gameObject;

        if (currentPanel == powerupPanel)
        {
            purchasedItem = shopPowerups[currentItemIndex];
            price = purchasedItem.getPrice();
        }
        else
        {
            purchasedItem = shopAccessory[currentItemIndex];
            price = purchasedItem.getPrice();
        }

        if (userPoints >= price)
        {
            userPoints -= price;
            user.setPoints(userPoints); // update points in database
            pointsText.text = "Points: " + userPoints.ToString(); // update points on shop UI
            
            // get name of the current item and match it with item names in user's inventory
            string purchasedItemName = purchasedItem.getItemName();
            bool existingItem = false; // indicates if the purchased item exists in user's inventory

            for (int i = 0; i < userInventory.Count; i++)
            {
                int currentCount; // current count of purchased item in user's inventory
                if (userInventory[i].getItemName() == purchasedItemName)
                {
                    existingItem = true; // purchased item EXISTS in user's inventory
                    currentCount = userInventory[i].getItemCount();
                    currentCount++;
                    userInventory[i].setItemCount(currentCount); // updates count in user's inventory
                    break;
                }
            }

            if (!existingItem) // if purchased item DOES NOT EXIST in user's inventory
            {
                int purchasedItemCount;
                userInventory.Add(purchasedItem); // adds purchased item into user's inventory
                purchasedItemCount = userInventory[userInventory.Count - 1].getItemCount();
                purchasedItemCount++; // set item count to 1
                userInventory[userInventory.Count - 1].setItemCount(purchasedItemCount); // updates count in user's inventory 
                user.setInventory(userInventory); // updates user's inventory
            }

            print("Purchase successful!");
            warningRegisterText.gameObject.SetActive(true);
        }
        else
        {
            print("Not enough points!");
        }
        UpdateUserInventory(url_user, user);
    }
}