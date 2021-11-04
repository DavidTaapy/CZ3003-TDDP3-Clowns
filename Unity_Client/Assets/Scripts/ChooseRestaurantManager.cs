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
                Debug.Log("Fine Dining Chosen");
                break;
            case 1:
                Debug.Log("Diner Chosen");
                break;
            case 2:
                Debug.Log("Cafe Chosen");
                break;
            default:
                Debug.Log("Something Went Wrong");
                break;
        }
    }
}
