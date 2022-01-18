using TMPro;
using UnityEngine;

namespace PlayFabDemo
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameObject apple;
        public TextMeshProUGUI uiScore;
        [SerializeField] protected int score;

        private void Awake()
        {
            if (GameManager.instance != null) Debug.LogError("GameManager Error");
            GameManager.instance = this;
        }

        public virtual void GameStart()
        {
            this.apple.SetActive(true);
        }

        public virtual void ScoreAdd(int score)
        {
            this.score += score;
            string text = this.score.ToString();
            this.uiScore.text = text;
        }
    }

}