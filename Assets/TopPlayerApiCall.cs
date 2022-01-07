using SaiHttpRequest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPlayerApiCall : JsonBinApiCall
{
    protected string jsonId = "61d85a652675917a628bee24";

    public virtual string Uri()
    {
        return "/b/" + this.jsonId;
    }
}
