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
    private string resSource;

    [SerializeField]
    private Dictionary<string , List<string>> Dishes;

    public List<string> getDishes(string dish){
        return Dishes[dish];
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
    public string getResSource()
    {
        return this.resSource;
    }

    public void setResSource(string resSource)
    {
        this.resSource = resSource;
    }

    public string ToJSON(){
        return JsonUtility.ToJson(this);
    }


}