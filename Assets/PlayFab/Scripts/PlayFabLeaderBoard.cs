using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

namespace PlayFabDemo
{
    public class PlayFabLeaderBoard : MonoBehaviour
    {
        public static PlayFabLeaderBoard instance;

        [Header("Top Clicker")]
        public string topClickerStatistic = "Clicker";
        public List<PlayerLeaderboardEntry> topClickers = new List<PlayerLeaderboardEntry>();

        [Header("Top Eater")]
        public string topEaterStatistic = "Eating";
        public List<PlayerLeaderboardEntry> topEaters = new List<PlayerLeaderboardEntry>();

        private void Awake()
        {
            PlayFabLeaderBoard.instance = this;
        }

        public virtual void GetTopClickers()
        {
            GetLeaderboardRequest request = new GetLeaderboardRequest
            {
                StatisticName = this.topClickerStatistic,
                StartPosition = 0,
                MaxResultsCount = 7
            };

            PlayFabClientAPI.GetLeaderboard(request, this.DisplayTopClicker, this.RequestError);
        }

        protected virtual void DisplayTopClicker(GetLeaderboardResult result)
        {
            if (result == null) return;

            this.topClickers = result.Leaderboard;
            UITopClickers.instance.ShowTopPlayers(topClickers);
        }

        public virtual void GetClickCount()
        {
            GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest();
            PlayFabClientAPI.GetPlayerStatistics(request, this.OnGetStatistics, this.RequestError);
        }

        private void OnGetStatistics(GetPlayerStatisticsResult result)
        {
            StatisticValue score = result.Statistics.Find((x) => x.StatisticName == this.topClickerStatistic);
            if (score == null) return;
            GameManager.instance.bestScore = score.Value;
        }

        public virtual void UpdateTopClicker(int newScore)
        {
            UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate> {
                    new StatisticUpdate { StatisticName = this.topClickerStatistic, Value = newScore }
                }
            };

            PlayFabClientAPI.UpdatePlayerStatistics(request, this.UpdateTopClickerSuccess, this.RequestError);
        }

        protected virtual void UpdateTopClickerSuccess(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("UpdateTopClickerSuccess");
            Invoke("GetTopClickers", 2f);
        }

        protected virtual void RequestError(PlayFabError error)
        {
            string textError = error.GenerateErrorReport();
            Debug.LogWarning(textError);
        }

        public virtual void GetTopEaters()
        {
            GetLeaderboardRequest request = new GetLeaderboardRequest
            {
                StatisticName = this.topEaterStatistic,
                StartPosition = 0,
                MaxResultsCount = 7
            };

            PlayFabClientAPI.GetLeaderboard(request, this.DisplayTopEater, this.RequestError);
        }

        protected virtual void DisplayTopEater(GetLeaderboardResult result)
        {
            if (result == null) return;

            this.topEaters = result.Leaderboard;
            UITopEater.instance.ShowTopPlayers(this.topEaters);

            string playFabId = GameManager.instance.playFabId;
            PlayerLeaderboardEntry player = this.topEaters.Find((p) => p.PlayFabId == playFabId);
            GameManager.instance.ShowBites(player.StatValue);
            //TODO: has bug when more then 7 players
        }

        public virtual void LazyLoadTopEater()
        {
            Debug.Log("LazyLoadTopEater");
            Invoke("GetTopEaters", 2f);
        }
    }
}