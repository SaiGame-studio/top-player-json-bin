using TMPro;
using UnityEngine;

namespace PlayFabDemo
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameObject apple;
        public GameObject userInputs;
        public TextMeshProUGUI uiScore;
        public int score;
        public int bestScore;

        private void Awake()
        {
            if (GameManager.instance != null) Debug.LogError("GameManager Error");
            GameManager.instance = this;
        }

        public virtual void GameStart()
        {
            this.userInputs.SetActive(false);
            this.apple.SetActive(true);

            PlayFabLeaderBoard.instance.GetScore();
            PlayFabLeaderBoard.instance.GetLeaderBoard();
            PlayFabInventory.instance.GetInventory();
        }

        public virtual void ScoreAdd(int addScore)
        {
            this.score += addScore;
            string text = this.score.ToString();
            this.uiScore.text = text;

            if (this.score <= this.bestScore) return;
            this.bestScore = this.score;

            PlayFabLeaderBoard.instance.UpdateScore(this.score);
        }
    }

}