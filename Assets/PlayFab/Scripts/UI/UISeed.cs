using TMPro;
using UnityEngine;

namespace PlayFabDemo
{
    public class UISeed : MonoBehaviour
    {
        public static UISeed instance;
        public TextMeshProUGUI seedText;

        private void Awake()
        {
            if (UISeed.instance != null) Debug.LogError("UITopPlayers Error");
            UISeed.instance = this;

            this.LoadTexts();
        }

        protected virtual void LoadTexts()
        {
            this.seedText = GetComponent<TextMeshProUGUI>();
        }

        public virtual void Show(int seed)
        {
            this.seedText.text = "Seed: " + seed;
        }
    }
}