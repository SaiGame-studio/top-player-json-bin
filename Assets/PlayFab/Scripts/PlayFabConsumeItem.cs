using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayFabDemo
{
    public class PlayFabConsumeItem : MonoBehaviour
    {
        public static PlayFabConsumeItem instance;

        private void Awake()
        {
            if (PlayFabConsumeItem.instance != null) Debug.LogError("PlayFabConsumeItem Error");
            PlayFabConsumeItem.instance = this;
        }

        public virtual void Consume(string itemId)
        {

            ItemInstance itemInstance = PlayFabInventory.instance.FindById(itemId);
            if (itemInstance == null)
            {
                Debug.LogWarning("Item not found: " + itemId);
                UIInventory.instance.Show(itemId, 0);
                return;
            }

            ConsumeItemRequest request = new ConsumeItemRequest
            {
                ConsumeCount = 1,
                ItemInstanceId = itemInstance.ItemInstanceId,
            };

            PlayFabClientAPI.ConsumeItem(request, this.ConsumeItemSuccess, this.RegisterError);
        }

        protected virtual void ConsumeItemSuccess(ConsumeItemResult result)
        {
            Debug.Log("ConsumeItemSuccess: " + result.ItemInstanceId + " - " + result.RemainingUses);
            PlayFabInventory.instance.LazyLoadInventory();
        }

        protected virtual void RegisterError(PlayFabError error)
        {
            string textError = error.GenerateErrorReport();
            Debug.LogWarning(textError);
        }
    }
}