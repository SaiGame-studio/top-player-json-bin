using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayFabDemo
{
    public class PlayFabPurchaseItem : MonoBehaviour
    {
        public static PlayFabPurchaseItem instance;

        private void Awake()
        {
            if (PlayFabPurchaseItem.instance != null) Debug.LogError("PlayFabPurchaseItem Error");
            PlayFabPurchaseItem.instance = this;
        }

        public virtual void Purchase(string itemId, int price, string currency = "SE")
        {
            PurchaseItemRequest request = new PurchaseItemRequest
            {
                ItemId = itemId,
                Price = price,
                VirtualCurrency = currency,
            };

            PlayFabClientAPI.PurchaseItem(request, this.PurchaseItemSuccess, this.RegisterError);
        }

        protected virtual void PurchaseItemSuccess(PurchaseItemResult result)
        {
            foreach (ItemInstance itemInstance in result.Items)
            {
                Debug.Log(itemInstance.ItemId + " - " + itemInstance.DisplayName + ": " + itemInstance.RemainingUses);
            }

            PlayFabInventory.instance.GetInventory();
        }

        protected virtual void RegisterError(PlayFabError error)
        {
            string textError = error.GenerateErrorReport();
            Debug.LogWarning(textError);
        }
    }
}