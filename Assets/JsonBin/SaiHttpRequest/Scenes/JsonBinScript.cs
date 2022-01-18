using UnityEngine;
using UnityEngine.Networking;

public class JsonBinScript : MonoBehaviour
{
    protected JsonBinApiCall apiCall = new JsonBinApiCall();

    // Start is called before the first frame update
    private void Start()
    {
        this.apiCall.isDebug = true;

        this.GetBinNotExist();        
        //this.CreateBin(); //Need Master Key to try
    }

    protected virtual void GetBinNotExist()
    {
        Debug.Log("== GetBinNotExist =============");
        string uri = "/b/not-exist";
        StartCoroutine(this.apiCall.JsonGet(uri));
    }

    protected virtual void CreateBin()
    {
        Debug.Log("== CreateBin =============");
        string uri = "/b";

        string jsonString = "{\"unity\":\"hello\"}";
        StartCoroutine(this.apiCall.JsonPost(uri, jsonString, this.OnCreateBin));
    }

    protected virtual void OnCreateBin(UnityWebRequest request, string jsonResponse)
    {
        Debug.Log("Json Response: " + jsonResponse);

        JsonBinMetaDataRes res = JsonBinMetaDataRes.FromJSON(jsonResponse);
        Debug.Log("Metadata: " + res.metadata.id);
        Debug.Log("Metadata: " + res.metadata.createdAt);

        string uri = "/b/"+res.metadata.id;
        StartCoroutine(this.apiCall.JsonGet(uri));
    }
}


