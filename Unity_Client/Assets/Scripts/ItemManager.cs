using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public static int numberOfSlots = 14;

    public Item[] items = new Item[numberOfSlots];
    public ItemSlot[] slots = new ItemSlot[numberOfSlots];

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        // slots = FindObjectsOfType<ItemSlot>();
    }

    // Returns true if an item has been added to an empty slot
    private bool AddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                return true;
            }
        }
        return false;
    }

    // Checks if an item has been added to an empty slot and updates slot UI accordingly
    public void UpdateSlotUI(Item item)
    {
        bool hasAdded = AddItem(item);

        if (hasAdded)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].UpdateSlot();
            }
        }
        
    }

    // ChangeScene is called when user clicks 'View Items' or 'Change Character'
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
