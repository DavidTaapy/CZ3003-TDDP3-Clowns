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

    public void redirectToLogin()
    {
        Loader.Load(Loader.Scene.LoginFirebase);
    }

    public void redirectToRegister()
    {
        Loader.Load(Loader.Scene.RegisterFirebase);
    }

    public void redirectToMainMenu()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }

    public void redirectToMultiWin()
    {
        Loader.Load(Loader.Scene.MultiplayerWinScene);
    }

    public void redirectToMultiLose()
    {
        Loader.Load(Loader.Scene.MultiplayerLoseScene);
    }

    public void redirectToMultiMatchmaking()
    {
        Loader.Load(Loader.Scene.MP_Matched);
    }

    public void redirectToMultiFoundMatch()
    {
        Loader.Load(Loader.Scene.MP_Finding);
    }

    public void redirectToChooseRestaurant()
    {
        Loader.Load(Loader.Scene.ChooseRestaurantScene);
    }

    public void redirectToReport()
    {
        Loader.Load(Loader.Scene.ReportScene);
    }

    public void redirectToSeasonRewards()
    {
        Loader.Load(Loader.Scene.ViewSeasonRewardsScene);
    }

    public void redirectToUserProfile()
    {
        Loader.Load(Loader.Scene.UserProfileScene);
    }

    public void redirectToCharacter()
    {
        Loader.Load(Loader.Scene.CharacterScene);
    }

}
