using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UTTopPlayers : MonoBehaviour
{
    public static UTTopPlayers instance;
    public List<TextMeshProUGUI> textPlayerScores = new List<TextMeshProUGUI>();

    private void Awake()
    {
        if (UTTopPlayers.instance != null) Debug.LogError("UTTopPlayers Error");
        UTTopPlayers.instance = this;

        this.LoadTexts();
    }

    protected virtual void LoadTexts()
    {
        foreach (Transform child in transform)
        {
            TextMeshProUGUI textMeshPro = child.GetComponent<TextMeshProUGUI>();
            this.textPlayerScores.Add(textMeshPro);
        }
    }

    public virtual void ShowTopPlayers(string jsonStringResponse)
    {
        PlayerScoresRes playerScoresRes = PlayerScoresRes.FromJSON(jsonStringResponse);
        TopPlayers.instance.playerScores = playerScoresRes.record;

        int i = 0;
        TextMeshProUGUI textMeshPro;
        foreach (PlayerScore playerScore in TopPlayers.instance.playerScores.playerScores)
        {
            textMeshPro = this.textPlayerScores[i];

            string text = playerScore.name + " - " + playerScore.score;
            textMeshPro.text = text;
            i++;
        }
    }
}
