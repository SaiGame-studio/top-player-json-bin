using SaiHttpRequest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPlayerApiCall : JsonBinApiCall
{
    protected string jsonId = "61db0d7d39a33573b3260566";

    public virtual string Uri()
    {
        return "/b/" + this.jsonId;
    }
}
