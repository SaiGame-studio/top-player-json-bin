using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlayFabDemo
{
    public class UIInventory : MonoBehaviour
    {
        public static UIInventory instance;
        public List<TextMeshProUGUI> apples;

        private void Awake()
        {
            if (UIInventory.instance != null) Debug.LogError("UIInventory Error");
            UIInventory.instance = this;

            this.LoadTexts();
        }

        protected virtual void LoadTexts()
        {
            foreach(Transform child in transform)
            {
                TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
                this.apples.Add(text);
            }
        }

        public virtual void Show(string itemId, int number)
        {
            TextMeshProUGUI text = this.apples.Find((x) => x.transform.name == itemId);
            text.text = "x" + number;
        }
    }
}