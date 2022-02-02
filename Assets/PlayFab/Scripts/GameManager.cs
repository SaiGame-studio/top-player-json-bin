using TMPro;
using UnityEngine;

namespace PlayFabDemo
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public string username;
        public string playFabId;
        public GameObject apple;
        public GameObject userInputs;
        public TextMeshProUGUI uiClicks;
        public TextMeshProUGUI uiEating;
        public int score;
        public int bestScore;

        private void Awake()
        {
            if (GameManager.instance != null) Debug.LogError("GameManager Error");
            GameManager.instance = this;

            this.apple = GameObject.Find("Apple");
            this.apple.gameObject.SetActive(false);

            this.userInputs = GameObject.Find("UIUserInput");
            this.uiClicks = GameObject.Find("UIClicks").GetComponent<TextMeshProUGUI>();
            this.uiEating = GameObject.Find("UIEating").GetComponent<TextMeshProUGUI>();
        }

        public virtual void GameStart(string username, string playFabId)
        {
            this.username = username;
            this.playFabId = playFabId;
            this.userInputs.SetActive(false);
            this.apple.SetActive(true);

            PlayFabLeaderBoard.instance.GetClickCount();
            PlayFabLeaderBoard.instance.GetTopClickers();
            PlayFabLeaderBoard.instance.GetTopEaters();
            PlayFabInventory.instance.GetInventory();
        }

        public virtual void ScoreAdd(int addScore)
        {
            this.score += addScore;
            string text = this.score.ToString();
            this.uiClicks.text = text;

            if (this.score <= this.bestScore) return;
            this.bestScore = this.score;

            PlayFabLeaderBoard.instance.UpdateTopClicker(this.score);
        }

        public virtual void ShowBites(int bites)
        {
            this.uiEating.text = bites.ToString();
        }
    }

}