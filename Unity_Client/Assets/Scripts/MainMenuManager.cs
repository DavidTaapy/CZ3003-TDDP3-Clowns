using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    
    public void redirectToLeaderboard()
    {
        Loader.Load(Loader.Scene.LeaderboardScene);
    }

}
