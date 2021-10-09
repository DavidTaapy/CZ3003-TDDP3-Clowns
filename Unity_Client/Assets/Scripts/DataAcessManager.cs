using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace apiManagers{
    public class DataAcessManager : MonoBehaviour
    {
        public string url;
        
        void Start(){
            // By name
            var userId = 2;
            var linktoUserGet = GameObject.FindWithTag("Dao").GetComponent<UserDao>();
            Debug.Log("============Starting Data Access Manager========");
            StartCoroutine(linktoUserGet.Get(url, userId));

            User user = new User(6, "Harry Potter", 400, 5);
            //StartCoroutine(linktoUserGet.Post(url, user));

            //StartCoroutine(linktoUserGet.Delete(url, 0));

            user.setEloRating(500000);
            //StartCoroutine(linktoUserGet.Put(url, user));
        }
    }
}

