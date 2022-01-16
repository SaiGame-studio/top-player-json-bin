using UnityEngine;
using System;

[Serializable]
public class JsonBinMetaDataRes
{
    //public string record;

    [SerializeField]
    public Metadata metadata;

    public static JsonBinMetaDataRes FromJSON(string jsonString)
    {
        return JsonUtility.FromJson<JsonBinMetaDataRes>(jsonString);
    }
}

[Serializable]
public class Metadata
{
    public string id;
    public string createdAt;
    //public string private;//TODO: DIY
}