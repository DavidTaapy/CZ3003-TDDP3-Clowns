using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    private string userId = "7HHcjbfJq1kD8VFMHHDq";
    private string url_user = "http://localhost:3000/user";
    private string url_characters = "http://localhost:3000/allcharacter";

    [Header("Character Details")]
    private User user;
    private List<Character> characters;
    private Character currentCharacter;
    private string ID;
    public Text name;
    public Text description;
    public Image image;
    private Sprite sprite;
    private int selectedOption = 0;

    void Start()
    {
        user = GetUserDetails(url_user, userId);
        characters = GetAllCharacterDetails(url_characters);
        currentCharacter = characters[selectedOption];
        UpdateCharacter(currentCharacter);
    }

    private User GetUserDetails(string url_user, string userId){
        var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
        User user = linktoUserGet.getUser(url_user, userId);
        return user;
    }

    private List<Character> GetAllCharacterDetails(string url){
        var linktoAllCharactersGet = GameObject.Find("CharacterDao").GetComponent<CharacterDao>();
        List<Character> allCharacters = linktoAllCharactersGet.getAllCharacters(url);
        return allCharacters;
    }

    private void UpdateCharacter(Character character)
    {
        ID = character.getCharacterID();
        name.text = character.getCharacterName();
        description.text = character.getCharacterDescription();
        sprite = Resources.Load<Sprite>(character.getSpriteSource());
        image.transform.GetComponent<Image>().sprite = sprite;
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= characters.Count)
        {
            selectedOption = 0;
        }

        currentCharacter = characters[selectedOption];
        UpdateCharacter(currentCharacter);
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = characters.Count - 1;
        }

        currentCharacter = characters[selectedOption];
        UpdateCharacter(currentCharacter);
    }

    public void SelectCharacter()
    {
        user.setCharacter(currentCharacter);
        print("Character selected successfully!");
    }
}
