using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net.Http;
using UnityEngine;
using Newtonsoft.Json;

public class ItemDao : MonoBehaviour
{
    // http://localhost:3000/items
    
    HttpClient client = new HttpClient();

    public List<Item> getItems(string url, string itemType, string itemSource){
        
        string urlWithParams = string.Format("{0}?itemType={1}&itemSource={2}", url, itemType, itemSource);

        HttpResponseMessage response = client.GetAsync(urlWithParams).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        List<Item> itemList = JsonConvert.DeserializeObject<List<Item>>(responseStr);
        return itemList;
    }
}