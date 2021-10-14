using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HistoryManager : MonoBehaviour
{
    public Text qn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*void DisplayQuestions()
    {
        for (int i = 0; i < userQuestions.items.Count; i++)
        {
            if (i >= pageID * itemPerPage && i < (pageID + 1) * itemPerPage)
            {
                inventorySlots[i].gameObject.SetActive(true);
                inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventoryManager.items[i].itemSprite;
                inventorySlots[i].transform.GetChild(1).GetComponent<Text>().text = inventoryManager.items[i].itemCount.ToString();
                inventorySlots[i].transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                inventorySlots[i].gameObject.SetActive(false);
            }
        }
    }*/
}
