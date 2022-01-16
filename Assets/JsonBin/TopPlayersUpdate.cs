using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TopPlayersUpdate : MonoBehaviour
{
    public static TopPlayersUpdate instance;
    protected TopPlayerApiCall apiCall = new TopPlayerApiCall();
    public GameObject apple;
    public string playerName = null;
    public TMP_InputField playerNameInput;
    public GameObject inputPlayer;

    private void Awake()
    {
        if (TopPlayersUpdate.instance != null) Debug.LogError("TopPlayers Error");
        TopPlayersUpdate.instance = this;
    }

    public virtual void GetAndUpdateTopPlayers()
    {
        //this.apiCall.isDebug = true;
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

        UTTopPlayers.instance.ShowTopPlayers(jsonStringResponse);
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

        UTTopPlayers.instance.ShowTopPlayers(jsonStringResponse);
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


    /// <summary>
    /// Call from Button UI
    /// </summary>
    public virtual void GetName()
    {
        this.playerName = this.playerNameInput.text;
        if (this.playerName == null) return;
        if (this.playerName == "") return;

        this.inputPlayer.SetActive(false);
        this.apple.SetActive(true);
    }
}
