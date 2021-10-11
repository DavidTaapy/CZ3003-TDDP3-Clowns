using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public Text nameText;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }

        UpdateCharacter(selectedOption);
    }

    // NextOption is called when user clicks 'Next'
    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    // BackOption is called when user clicks 'Back'
    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0 )
        {
            selectedOption = characterDB.CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
        Save();
    }

    // UpdateCharacter is called to update displayed sprite and name
    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        nameText.text = character.characterName;
    }

    // Loads previously selected character
    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    // Saves current selected character
    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    // ChangeScene is called when user clicks 'Select'
    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
