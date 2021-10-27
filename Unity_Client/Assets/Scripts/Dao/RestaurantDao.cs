using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net.Http;
using UnityEngine;
using Newtonsoft.Json;

public class RestaurantDao : MonoBehaviour
{
    // http://localhost:3000/restaurant
    
    HttpClient client = new HttpClient();

    public List<Restaurant> getRestaurant(string url, string restaurantName){
        
        string urlWithParams = string.Format("{0}?name={1}", url, restaurantName);

        HttpResponseMessage response = client.GetAsync(urlWithParams).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        List<Restaurant> restaurant = JsonConvert.DeserializeObject<List<Restaurant>>(responseStr);
        return restaurant;
    }
}