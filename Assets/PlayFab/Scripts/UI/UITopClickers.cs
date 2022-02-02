using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlayFabDemo
{
    public class UITopClickers : MonoBehaviour
    {
        public static UITopClickers instance;
        public List<TextMeshProUGUI> textPlayerScores = new List<TextMeshProUGUI>();

        private void Awake()
        {
            if (UITopClickers.instance != null) Debug.LogError("UITopClickers Error");
            UITopClickers.instance = this;

            this.LoadTexts();
        }

        protected virtual void LoadTexts()
        {
            foreach (Transform child in transform)
            {
                TextMeshProUGUI textMeshPro = child.GetComponent<TextMeshProUGUI>();
                this.textPlayerScores.Add(textMeshPro);
            }
        }

        public virtual void ShowTopPlayers(List<PlayerLeaderboardEntry> leaderboard)
        {
            TextMeshProUGUI textMeshPro;
            foreach (PlayerLeaderboardEntry playerScore in leaderboard)
            {
                textMeshPro = this.textPlayerScores[playerScore.Position];
                string text = playerScore.DisplayName + " - " + playerScore.StatValue;
                textMeshPro.text = text;
            }
        }
    }
}