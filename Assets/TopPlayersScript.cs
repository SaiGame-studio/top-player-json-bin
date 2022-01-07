using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TopPlayersScript : MonoBehaviour
{
    public static TopPlayersScript instance;
    protected TopPlayerApiCall apiCall = new TopPlayerApiCall();

    [SerializeField]
    public PlayerScores playerScores = new PlayerScores();
    public List<TextMeshProUGUI> textPlayerScores = new List<TextMeshProUGUI>();
    public GameObject apple;
    public string playerName = null;
    public TMP_InputField playerNameInput;
    public GameObject inputPlayer;

    private void Awake()
    {
        if (TopPlayersScript.instance != null) Debug.LogError("TopPlayers Error");
        TopPlayersScript.instance = this;
    }

    public virtual void UpdateTopPlayers(int currentScore)
    {
        this.playerScores.AddPlayer(this.CurrentPlayer(currentScore));
        this.playerScores.TopUpdate();

        if (!this.playerScores.HasUpdate()) return;

        this.playerScores.oldMd5 = this.playerScores.newMd5;
        string json = JsonConvert.SerializeObject(this.playerScores);
        StartCoroutine(this.apiCall.JsonPut(this.apiCall.Uri(), json, this.OnUpdateDone));
    }

    public virtual void OnUpdateDone(UnityWebRequest request, string jsonStringResponse)
    {
        UnityWebRequest.Result re = request.result;
        if (re != UnityWebRequest.Result.Success)
        {
            //TODO: need more work here
            Debug.LogWarning(jsonStringResponse);
            return;
        }

        this.ShowTopPlayers(jsonStringResponse);
    }

    protected virtual PlayerScore CurrentPlayer(int currentScore)
    {
        PlayerScore playerScore = new PlayerScore
        {
            name = this.playerName,
            score = currentScore,
        };
        return playerScore;
    }

    public virtual void GetTopPlayers()
    {
        StartCoroutine(this.apiCall.JsonGet(this.apiCall.Uri(), "{}", this.OnGetTopPlayersDone));
    }

    public virtual void OnGetTopPlayersDone(UnityWebRequest request, string jsonStringResponse)
    {

        UnityWebRequest.Result re = request.result;
        if (re != UnityWebRequest.Result.Success)
        {
            //TODO: need more work here
            Debug.LogWarning(jsonStringResponse);
            return;
        }

        this.ShowTopPlayers(jsonStringResponse);
    }

    public virtual void ShowTopPlayers(string jsonStringResponse)
    {
        PlayerScoresRes playerScoresRes = PlayerScoresRes.FromJSON(jsonStringResponse);
        this.playerScores = playerScoresRes.record;

        int i = 0;
        TextMeshProUGUI textMeshPro;
        foreach (PlayerScore playerScore in this.playerScores.playerScores)
        {
            textMeshPro = this.textPlayerScores[i];

            string text = playerScore.name + " - " + playerScore.score;
            textMeshPro.text = text;
            i++;
        }
    }

    public virtual void GetName()
    {
        this.playerName = this.playerNameInput.text;
        if (this.playerName == null) return;
        if (this.playerName == "") return;

        this.inputPlayer.SetActive(false);
        this.apple.SetActive(true);
    }
}
