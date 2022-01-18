using TMPro;
using UnityEngine;

public class ClickMe : MonoBehaviour
{
    public static ClickMe instance;
    public int score = 0;
    public TextMeshProUGUI text;

    private void Awake()
    {
        if (ClickMe.instance != null) Debug.LogError("ClickMe Error");
        ClickMe.instance = this;
    }

    private void OnEnable()
    {
        TopPlayersGet.instance.Get();
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

        string text = TopPlayersUpdate.instance.playerName + ": " + this.score.ToString();
        this.text.text = text;

        if (this.score % 10 != 0) return;

        TopPlayersUpdate.instance.GetAndUpdateTopPlayers();
    }
}
