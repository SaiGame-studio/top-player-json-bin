using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

[Serializable]
public class PlayerScores
{
    [SerializeField]
    public List<PlayerScore> playerScores = new List<PlayerScore>();
    public string oldMd5 = "old";
    public string newMd5 = "new";

    public virtual void AddPlayer(PlayerScore newPlayerScore)
    {
        PlayerScore exist = null;
        foreach(PlayerScore playerScore in this.playerScores)
        {
            if (playerScore.name != newPlayerScore.name) continue;
            exist = playerScore;
            break;
        }

        if (exist == null)
        {
            this.playerScores.Add(newPlayerScore);
            return;
        }

        if (exist.score > newPlayerScore.score) return;

        exist.score = newPlayerScore.score;
    }

    public virtual void TopUpdate()
    {
        this.playerScores.Sort((p1, p2) => p1.score.CompareTo(p2.score));
        this.playerScores.Reverse();

        while(this.playerScores.Count > 7)
        {
            this.playerScores.RemoveAt(7);
        }

        string json = JsonConvert.SerializeObject(this.playerScores);
        //Debug.Log(json);
        this.newMd5 = this.MD5Hash(json);
    }

    public virtual bool HasUpdate()
    {
        return this.oldMd5 != this.newMd5;
    }

    protected string MD5Hash(string input)
    {
        StringBuilder hash = new StringBuilder();
        MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
        byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

        for (int i = 0; i < bytes.Length; i++)
        {
            hash.Append(bytes[i].ToString("x2"));
        }
        return hash.ToString();
    }
}
