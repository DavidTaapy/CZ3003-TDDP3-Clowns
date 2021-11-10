using System;
using APIs;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

namespace Handlers
{
    public class MatchmakingSceneHandler : MonoBehaviour
    {
        public GameObject searchingPanel;
        public GameObject foundPanel;
        public TMP_Text gameStartText;
        public TMP_Text gameStartText3;
        public TMP_Text gameStartText2;
        public TMP_Text gameStartText1;
        public Image backgroundColorForGameStart;

        private bool gameFound;
        private bool readyingUp;
        private string gameId;

        private void Start() {
            MainManager.Instance.currentLocalPlayerId = PlayerPrefs.GetString("uid");
            JoinQueue();
        }

        private void JoinQueue() =>
            MainManager.Instance.matchmakingManager.JoinQueue(MainManager.Instance.currentLocalPlayerId, gameId =>
                {
                    this.gameId = gameId;
                    gameFound = true;
                },
                Debug.Log);

        private void Update()
        {
            if (!gameFound || readyingUp) return;
            readyingUp = true;
            GameFound();
        }

        private IEnumerator gameStartCountDown() {
            searchingPanel.SetActive(false);
            foundPanel.SetActive(true);
            backgroundColorForGameStart.gameObject.SetActive(true);
            gameStartText.gameObject.SetActive(true);
            gameStartText3.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameStartText3.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            gameStartText2.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameStartText2.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            gameStartText1.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameStartText1.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Loader.Load(Loader.Scene.MultiMode);
        }

        private void GameFound()
        {
            MainManager.Instance.gameManager.GetCurrentGameInfo(gameId, MainManager.Instance.currentLocalPlayerId,
                gameInfo =>
                {
                    Debug.Log("Game found. Ready-up!");
                    gameFound = true;
                    MainManager.Instance.gameManager.ListenForAllPlayersReady(gameInfo.playersIds,
                        playerId => Debug.Log(playerId + " is ready!"), () =>
                        {
                            Debug.Log("All players are ready!");
                            SceneManager.LoadScene("GameScene");
                        }, Debug.Log);
                }, Debug.Log);

            StartCoroutine(gameStartCountDown());
        }

        public void LeaveQueue()
        {
            if (gameFound) MainManager.Instance.gameManager.StopListeningForAllPlayersReady();
            else
                MainManager.Instance.matchmakingManager.LeaveQueue(MainManager.Instance.currentLocalPlayerId,
                    () => Debug.Log("Left queue successfully"), Debug.Log);
            SceneManager.LoadScene("MenuScene");
        }

        public void Ready() =>
            MainManager.Instance.gameManager.SetLocalPlayerReady(() => Debug.Log("You are now ready!"), Debug.Log);
    }
}