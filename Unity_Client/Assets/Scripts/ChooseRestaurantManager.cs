using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseRestaurantManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int choice = 0;
    public GameObject CharacterImage;
    Sprite sprite;

    void Start()
    {
        var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        Debug.Log("============Starting Data Access Manager========");
        User user = linktoUserGet.getUser("http://localhost:3000/user", PlayerPrefs.GetString("uid"));
        Debug.Log(user.getCharacter().getSpriteSource());
        sprite = Resources.Load<Sprite>(user.getCharacter().getSpriteSource());
        CharacterImage.GetComponent<Image>().sprite = sprite;
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
