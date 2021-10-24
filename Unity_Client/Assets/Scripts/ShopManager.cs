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
        userInventory = user.getInventory();

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
        List<Item> shopAccessory = linktoItems.getItems(url_items, "Accessory", "Shop"); //returns list of accessory in shop
        Debug.Log("\n num of shop skins: " + shopAccessory.Count);
        foreach (Item i in shopAccessory)
        {
            Debug.Log(i.ToJSON());
        }
        return shopAccessory;
    }

    private void updateUserInventory(string url, User user) {
        var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        linktoUserGet.updateUser(url, user);
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

    void displayAccessory()
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

    void updateItemPanel(GameObject currentItem)
    {
        int currentItemIndex = currentItem.transform.GetSiblingIndex();
        GameObject currentPanel = currentItem.transform.parent.gameObject;

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

    public void purchaseItem()
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
        }
        else
        {
            print("Not enough points!");
        }
        updateUserInventory(url_user, user);
    }
}
