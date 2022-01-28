using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlayFabDemo
{

    public class Apple : MonoBehaviour
    {
        public static Apple instance;
        [SerializeField] protected int score = 1;

        private void Awake()
        {
            if (Apple.instance != null) Debug.LogError("Apple Error");
            Apple.instance = this;
        }

        private void OnMouseDown()
        {
            Click();
        }

        public virtual void Click()
        {
            Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            transform.position = randomPositionOnScreen;
            GameManager.instance.ScoreAdd(this.score);
            PlayFabInventory.instance.AddSeed(this.score);
        }
    }
}
