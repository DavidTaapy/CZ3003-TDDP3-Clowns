using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{

    private string url_items;
    private string url_user;
    public List<Item> shopPowerups;
    public List<Item> shopAccessory;
    public string userId = "7HHcjbfJq1kD8VFMHHDq";
    public User user;
    public GameObject[] shopSlots;
    private int itemPerPage = 8;
    public int pageID;
    public Text pageText;
    public GameObject powerupPanel;

    public GameObject accessoryPanel;
    public GameObject itemPanel;

    public bool isPowerups = true;
    // Start is called before the first frame update
    void Start()
    {
        user = getUserDetails(url_user, userId);
        shopPowerups = getShopPowerups(url_items);
        shopAccessory = getShopAccessory(url_items); 
        displayPowerups();
        powerupPanel.gameObject.SetActive(true);
        itemPanel.gameObject.SetActive(false);
        accessoryPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        updatePage();
    }

    private User getUserDetails(string url_user, string userId){
        var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        User user = linktoUserGet.getUser(url_user, userId);
        return user;
    }

    void displayPowerups()
    {
        for (int i = 0; i < itemPerPage; i++)
        {
            if (i >= pageID * itemPerPage && i < (pageID + 1) * itemPerPage)
            {
                shopSlots[i].gameObject.SetActive(true);
                shopSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = shopPowerups[i].itemSprite;
                shopSlots[i].transform.GetChild(1).GetComponent<Text>().text = shopPowerups[i].itemCount.ToString();
                shopSlots[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                shopSlots[i].gameObject.SetActive(false);
            }
        }
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

    private void updatePage(){
        pageText.text = (pageID + 1) + "/" + (Mathf.FloorToInt((shopSlots.Length - 1) / itemPerPage) + 1).ToString();
    }
    
    public void activateItemPanel()
    {
        GameObject currentItemButton = EventSystem.current.currentSelectedGameObject;
        updateItemPanel(currentItemButton);
        powerupPanel.gameObject.SetActive(false);
        itemPanel.gameObject.SetActive(true);
    }

    public void activateShopPanel()
    {
        powerupPanel.gameObject.SetActive(true);
        itemPanel.gameObject.SetActive(false);
    }

    void updateItemPanel(GameObject currentItemButton)
    {
        GameObject currentItem = currentItemButton.transform.parent.gameObject;
        int currentItemIndex = currentItem.transform.GetSiblingIndex();

        itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = shopPowerups[currentItemIndex].itemSprite;
        itemPanel.transform.GetChild(1).GetComponent<Text>().text = "Name: " + shopPowerups[currentItemIndex].itemName;
        itemPanel.transform.GetChild(2).GetComponent<Text>().text = "Price: " + shopPowerups[currentItemIndex].price.ToString();
        itemPanel.transform.GetChild(3).GetComponent<Text>().text = shopPowerups[currentItemIndex].itemDescription.ToString();
    }

    // TODO: buy item and update user inventory, substract userpoints by price
    void purchaseItem(int price, User user){

    }
}
