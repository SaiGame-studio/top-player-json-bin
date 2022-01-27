using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

namespace PlayFabDemo
{
    public class PlayFabLeaderBoard : MonoBehaviour
    {
        public static PlayFabLeaderBoard instance;
        public int bestScore = 0;

        private void Awake()
        {
            PlayFabLeaderBoard.instance = this;
        }

        public virtual void GetLeaderBoard()
        {
            GetLeaderboardRequest request = new GetLeaderboardRequest
            {
                StatisticName = "score",
                StartPosition = 0,
                MaxResultsCount = 7
            };

            PlayFabClientAPI.GetLeaderboard(request, this.DisplayLeaderboard, this.RequestError);
        }

        protected virtual void DisplayLeaderboard(GetLeaderboardResult result)
        {
            UTTopPlayers.instance.ShowTopPlayers(result.Leaderboard);
        }


        public virtual void GetScore()
        {
            GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest();
            PlayFabClientAPI.GetPlayerStatistics(request, this.OnGetStatistics, this.RequestError);
        }

        private void OnGetStatistics(GetPlayerStatisticsResult result)
        {
            StatisticValue score = result.Statistics.Find((x) => x.StatisticName == "score");
            if (score == null) return;
            this.bestScore = score.Value;
        }

        public virtual void UpdateScore(int newScore)
        {
            if (newScore <= this.bestScore) return;
            this.bestScore = newScore;

            UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "score", Value = newScore } }
            };

            PlayFabClientAPI.UpdatePlayerStatistics(request, this.UpdateScoreSuccess, this.RequestError);
        }

        protected virtual void UpdateScoreSuccess(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("UpdateScoreSuccess");
            this.GetLeaderBoard();
        }

        protected virtual void RequestError(PlayFabError error)
        {
            string textError = error.GenerateErrorReport();
            Debug.LogWarning(textError);
        }
    }
}