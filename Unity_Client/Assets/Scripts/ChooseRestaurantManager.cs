using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRestaurantManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int choice = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseFineDining()
    {
        choice = 0;
    }

    public void ChooseDiner()
    {
        choice = 1;
    }

    public void ChooseCafe()
    {
        choice = 2;
    }

    public void ChooseRestaurant()
    {
        switch(choice)
        {
            case 0:
                PlayerPrefs.SetString("RestaurantChoice", "FineDining");
                Debug.Log(PlayerPrefs.GetString("RestaurantChoice"));
                break;
            case 1:
                PlayerPrefs.SetString("RestaurantChoice", "Diner");
                Debug.Log(PlayerPrefs.GetString("RestaurantChoice"));
                break;
            case 2:
                PlayerPrefs.SetString("RestaurantChoice", "Cafe");
                Debug.Log(PlayerPrefs.GetString("RestaurantChoice"));
                break;
            default:
                Debug.Log("Something Went Wrong");
                break;
        }
    }
}
