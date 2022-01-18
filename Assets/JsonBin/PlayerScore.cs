using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerScore
{
    public string name;
    public int score;

    static int SortByScore(PlayerScore p1, PlayerScore p2)
    {
        return p1.score.CompareTo(p2.score);
    }
}
