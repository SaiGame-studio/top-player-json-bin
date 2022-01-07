using TMPro;
using UnityEngine;

public class ClickMe : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        TopPlayersScript.instance.GetTopPlayers();
    }

    private void OnMouseDown()
    {
        Click();
    }

    public virtual void Click()
    {
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        transform.position = randomPositionOnScreen;
        this.score += 1;

        string text = TopPlayersScript.instance.playerName + ": " + this.score.ToString();
        this.text.text = text;

        TopPlayersScript.instance.GetTopPlayers();
        TopPlayersScript.instance.UpdateTopPlayers(this.score);
    }
}
