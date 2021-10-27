using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using Newtonsoft.Json;

public class RewardsManager : MonoBehaviour
{
    public GameObject firstPlaceChar;
    public GameObject secondPlaceChar;
    public GameObject thirdPlaceChar;

    public string url_items = "http://localhost:3000/items";
    ItemDao linkToItems;
    List<Item> rewardsList;

    void Awake()
    {
        linkToItems = GameObject.Find("ItemDao").GetComponent<ItemDao>();
        rewardsList = linkToItems.getItems(url_items,"Accessory", "Leaderboard");
        displayRewards();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void displayRewards(){
        foreach (Item item in rewardsList) {
            string description = item.getItemDescription();
            var sprite = Resources.Load<Sprite>(item.getSpriteSource());
            if (description.Contains("first")){
                firstPlaceChar.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            } else if (description.Contains("second")){
                secondPlaceChar.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            } else {
                thirdPlaceChar.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            }

        }
    }
}
