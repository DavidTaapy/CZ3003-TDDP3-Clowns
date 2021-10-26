using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public Sprite characterSprite;
    public string characterID;
    public string characterName;
    public string characterDescription;

    public string spriteSource;

    Character(){
    }

    public string ToJSON()
    {
        return JsonUtility.ToJson(this);
    }

    public string getCharacterID()
    {
        return this.characterID;
    }

    public void setCharacterID(string characterID)
    {
        this.characterID = characterID;
    }

    public string getCharacterName()
    {
        return this.characterName;
    }

    public void setCharacterName(string characterName)
    {
        this.characterName = characterName;
    }

    public string getCharacterDescription()
    {
        return this.characterDescription;
    }

    public void setCharacterDescription(string characterDescription)
    {
        this.characterDescription = characterDescription;
    }

    public string getSpriteSource()
    {
        return this.spriteSource;
    }

    public void setSpriteSource(string spriteSource)
    {
        this.spriteSource = spriteSource;
    }
}
