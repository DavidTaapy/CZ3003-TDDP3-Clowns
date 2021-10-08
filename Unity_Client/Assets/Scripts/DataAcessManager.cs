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
            var linktoUserDAO = GameObject.FindWithTag("Dao").GetComponent<UserDao>();
            Debug.Log("in START");
            StartCoroutine(linktoUserDAO.Get(url));
        }
    }
}

