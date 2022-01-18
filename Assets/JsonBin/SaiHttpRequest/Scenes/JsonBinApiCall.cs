using SaiHttpRequest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JsonBinApiCall : ApiCall
{
    private readonly string masterKey = "$2b$10$Fy7y6dv6KdEmWh8TYK607ufWoL0OKntQADYRSPyi/7JFV6lnn8oym";

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string Domain()
    {
        return "api.jsonbin.io/v3";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    public override void SetHeaders(UnityWebRequest request)
    {
        base.SetHeaders(request);

        //TODO: set your own Master Key here
        request.SetRequestHeader("X-Master-Key", this.masterKey);
    }
}
