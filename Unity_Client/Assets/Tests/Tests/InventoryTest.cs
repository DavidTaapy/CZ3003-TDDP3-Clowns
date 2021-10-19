using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryTest
{
    [UnityTest]
    // Checks if the number of displayed inventory slots matches the number of items the user owns
    public IEnumerator display_items_on_start_only_display_owned_items()
    {
        SceneManager.LoadScene("InventoryScene");
        yield return new WaitForSeconds(3);

        GridLayoutGroup inventoryGrid = GameObject.Find("Inventory Panel").GetComponent<GridLayoutGroup>();
        int count = 0;
        for (int i = 0; i < inventoryGrid.transform.childCount; i++)
        {
            if (inventoryGrid.transform.GetChild(i).gameObject.activeSelf)
            {
                count++;
            }
        }

        Assert.AreEqual(1, count);
        yield return new WaitForSeconds(3);
    }
}
