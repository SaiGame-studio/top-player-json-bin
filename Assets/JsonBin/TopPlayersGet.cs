using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TopPlayersGet : MonoBehaviour
{
    public static TopPlayersGet instance;
    protected TopPlayerApiCall apiCall = new TopPlayerApiCall();

    private void Awake()
    {
        if (TopPlayersGet.instance != null) Debug.LogError("TopPlayers Error");
        TopPlayersGet.instance = this;
    }

    public virtual void Get()
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

        UTTopPlayers.instance.ShowTopPlayers(jsonStringResponse);
    }
}
