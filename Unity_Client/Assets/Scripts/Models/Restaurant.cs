using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Restaurant
{
    [SerializeField]
    private string id;

    [SerializeField]
    private string name;

    [SerializeField]
    private string spriteSource;

    [SerializeField]
    private List<string> dishes;

    public Restaurant(string id, string name, string spriteSource, List<string> dishes){
        this.id = id;
        this.name = name;
        this.spriteSource = spriteSource;
        this.dishes = dishes;
    }

    public string getSpriteSource()
    {
        return this.spriteSource;
    }

    public void setSpriteSource(string spriteSource)
    {
        this.spriteSource = spriteSource;
    }

    public List<string> getDishes()
    {
        return this.dishes;
    }

    public void setDishes(List<string> dishes)
    {
        this.dishes = dishes;
    }

    public string getId()
    {
        return this.id;
    }

    public void setId(string id)
    {
        this.id = id;
    }

    public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public string ToJSON(){
        return JsonUtility.ToJson(this);
    }
}