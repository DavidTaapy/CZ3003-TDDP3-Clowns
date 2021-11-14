using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class Loader
{

    public enum Scene
    {
        LoadingScene, 
        MainMenu, 
        LeaderboardScene, 
        InventoryScene, ShopScene,
        RegisterFirebase, LoginFirebase, 
        SingleMode, MultiMode, 
        MultiplayerWinScene, MultiplayerLoseScene, 
        MP_Matched, MP_Finding, ChooseRestuarantScene,
        ViewSeasonRewardsScene, ReportScene,
        UserProfileScene, CharacterScene,
        LastSeasonRewardScene
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {
        // Set the loader callback action to load the target screen
        onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };
        // Load the loading screen
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        // Triggered after the first update which lets the screen refresh
        // Execute the loader callback action which will load the target scene
        if (onLoaderCallback != null)
        {
            // Introducing a 10-cycle delay for loading
            int i = 5;
            while (i > 0)
            {
                onLoaderCallback();
                i--;
            }
            onLoaderCallback = null;
        }
    }
}