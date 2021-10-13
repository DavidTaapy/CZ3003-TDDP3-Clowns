using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace apiManagers{
    public class DataAcessManager : MonoBehaviour
    {
        public string url_user;
        public string url_qn;
        public string url_leaderboard;
        public string url_items;
        
        void Start(){
            // By name
            var userId = "7HHcjbfJq1kD8VFMHHDq";
            
            // Code to get user details
            
            var linktoUserGet = GameObject.Find("UserDao").GetComponent<UserDao>();
            Debug.Log("============Starting Data Access Manager========");
            Debug.Log(GameObject.FindWithTag("Dao"));
            User user = linktoUserGet.getUser(url_user, userId);
            Debug.Log(user.getId());
            
            // Code to create user
            //User user2 = new User("Harry Potter", 400, 5);
            //string result = linktoUserGet.createUser(url_user, user2);
            //Debug.Log(result);

            // Code to delete user
            //userId = "mlDuFwswQXuf6Hyo1JyY";
            //string res = linktoUserGet.deleteUser(url_user, userId);
            //Debug.Log(res);

            // Code to update user
            //User user2 = linktoUserGet.getUser(url_user, "7HHcjbfJq1kD8VFMHHDq");
            //user2.setEloRating(500000);
            //string result = linktoUserGet.updateUser(url_user, user2);
            //Debug.Log(result);

            // Code to get questions from db
            var linktoQuestionGet = GameObject.Find("QuestionDao").GetComponent<QuestionDao>();
            /*var primaryLevel = 4;
            List<QuestionSO> qnList = linktoQuestionGet.getQuestions(url_qn, primaryLevel);
            foreach (var qn in qnList)
            {
                Debug.Log(qn.ToJSON);
            }
            */


            // Code to get leaderboard ranking from db
            var linktoLeaderboard = GameObject.Find("LeaderboardDao").GetComponent<LeaderboardDao>();
            List<User> userRanking = linktoLeaderboard.getLeaderboard(url_leaderboard);
            Debug.Log(userRanking.Count);
            foreach (User u in userRanking)
            {
                Debug.Log(u.ToJSON());
            }
            

            // Code to get shop items (skins and powerups separately) from from db
            var linktoItems = GameObject.Find("ItemsDao").GetComponent<ItemDao>();
            List<Item> shopPowerUps = linktoItems.getItems(url_items, "Powerup", "Shop"); //returns list of powerups in shop
            List<Item> shopSkins = linktoItems.getItems(url_items, "Skin", "Shop"); //returns list of powerups in shop
            Debug.Log("\n num of shop powerup: " + shopPowerUps.Count);
            Debug.Log("\n num of shop skins: " + shopSkins.Count);
            foreach (Item i in shopPowerUps)
            {
                Debug.Log(i.ToJSON());
            }

            foreach (Item i in shopSkins)
            {
                Debug.Log(i.ToJSON());
            }
        }
    }
}

