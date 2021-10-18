using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public void redirectToLeaderboard()
    {
        Loader.Load(Loader.Scene.LeaderboardScene);
    }

    public void redirectToSingle()
    {
        Loader.Load(Loader.Scene.SingleMode);
    }

    public void redirectToMulti()
    {
        Loader.Load(Loader.Scene.MP_Finding);
    }

    public void redirectToShop()
    {
        Loader.Load(Loader.Scene.ShopScene);
    }

    public void redirectToInventory()
    {
        Loader.Load(Loader.Scene.InventoryScene);
    }

    public void logOut()
    {
        Loader.Load(Loader.Scene.LoginFirebase);
    }

}
