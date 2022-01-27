using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlayFabDemo
{
    public class UTTopPlayers : MonoBehaviour
    {
        public static UTTopPlayers instance;
        public List<TextMeshProUGUI> textPlayerScores = new List<TextMeshProUGUI>();

        private void Awake()
        {
            if (UTTopPlayers.instance != null) Debug.LogError("UTTopPlayers Error");
            UTTopPlayers.instance = this;

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