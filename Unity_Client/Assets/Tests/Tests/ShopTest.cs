using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopTest
{
    [UnityTest]
    // Checks if the number of displayed powerups matches the number of powerups in the database
    public IEnumerator display_powerups_on_start_display_all_powerups()
    {
        SceneManager.LoadScene("ShopScene");
        yield return new WaitForSeconds(3);

        GridLayoutGroup powerupGrid = GameObject.Find("Powerup Panel").GetComponent<GridLayoutGroup>();
        int count = 0;
        for (int i = 0; i < powerupGrid.transform.childCount; i++)
        {
            if (powerupGrid.transform.GetChild(i).gameObject.activeSelf)
            {
                count++;
            }
        }

        Assert.AreEqual(7, count);
        yield return new WaitForSeconds(3);
    }

    [UnityTest]
    // Checks if accessory page loads when 'Next' button is clicked
    public IEnumerator activate_accessory_panel_on_click_load_accessory_page()
    {
        SceneManager.LoadScene("ShopScene");
        yield return new WaitForSeconds(3);

        Button nextButton = GameObject.Find("Next Button").GetComponent<Button>();
        nextButton.onClick.Invoke();

        GameObject accessoryPanel = GameObject.Find("Accessory Panel");
        bool isActivated = accessoryPanel.activeSelf;
        Assert.AreEqual(true, isActivated);

        yield return new WaitForSeconds(3);
    }
}
