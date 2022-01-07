using System;
using UnityEngine;

[Serializable]
public class PlayerScoresRes
{
    public PlayerScores record;

    public static PlayerScoresRes FromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerScoresRes>(jsonString);
    }
}
