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
    //public PlayerScores playerScores = new PlayerScores();
    //public List<TextMeshProUGUI> textPlayerScores = new List<TextMeshProUGUI>();
    public GameObject apple;
    public string playerName = null;
    public TMP_InputField playerNameInput;
    public GameObject inputPlayer;

    private void Awake()
    {
        if (TopPlayersScript.instance != null) Debug.LogError("TopPlayers Error");
        TopPlayersScript.instance = this;
    }

    public virtual void GetAndUpdateTopPlayers()
    {
        this.apiCall.isDebug = true;
        StartCoroutine(this.apiCall.JsonGet(this.apiCall.Uri(), "{}", this.OnGet2UpdateDone));
    }

    public virtual void OnGet2UpdateDone(UnityWebRequest request, string jsonStringResponse)
    {

        UnityWebRequest.Result re = request.result;
        if (re != UnityWebRequest.Result.Success)
        {
            //TODO: need more work here
            Debug.LogWarning(jsonStringResponse);
            return;
        }

        this.ShowTopPlayers(jsonStringResponse);
        this.UpdateTopPlayers();
    }

    public virtual void UpdateTopPlayers()
    {
        TopPlayers.instance.playerScores.AddPlayer(this.CurrentPlayer());
        TopPlayers.instance.playerScores.TopUpdate();

        if (!TopPlayers.instance.playerScores.HasUpdate()) return;

        TopPlayers.instance.playerScores.oldMd5 = TopPlayers.instance.playerScores.newMd5;
        string json = JsonConvert.SerializeObject(TopPlayers.instance.playerScores);
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

    protected virtual PlayerScore CurrentPlayer()
    {
        PlayerScore playerScore = new PlayerScore
        {
            name = this.playerName,
            score = ClickMe.instance.score,
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
        TopPlayers.instance.playerScores = playerScoresRes.record;

        int i = 0;
        TextMeshProUGUI textMeshPro;
        foreach (PlayerScore playerScore in TopPlayers.instance.playerScores.playerScores)
        {
            textMeshPro = UTTopPlayers.instance.textPlayerScores[i];

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
