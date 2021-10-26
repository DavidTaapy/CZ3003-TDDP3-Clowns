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
}
