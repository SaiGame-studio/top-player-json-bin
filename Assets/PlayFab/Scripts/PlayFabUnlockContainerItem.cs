using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayFabDemo
{
    public class PlayFabUnlockContainerItem : MonoBehaviour
    {
        public static PlayFabUnlockContainerItem instance;

        private void Awake()
        {
            if (PlayFabUnlockContainerItem.instance != null) Debug.LogError("PlayFabUnlockContainerItem Error");
            PlayFabUnlockContainerItem.instance = this;
        }

        public virtual void Unlock(string itemId)
        {
            ItemInstance itemInstance = PlayFabInventory.instance.FindById(itemId);
            if (itemInstance == null)
            {
                Debug.LogWarning("Item not found: " + itemId);
                UIInventory.instance.Show(itemId, 0);
                return;
            }

            UnlockContainerItemRequest request = new UnlockContainerItemRequest
            {
                ContainerItemId = itemId,
            };

            PlayFabClientAPI.UnlockContainerItem(request, this.UnlockSuccess, this.RegisterError);
        }

        protected virtual void UnlockSuccess(UnlockContainerItemResult result)
        {
            foreach (ItemInstance itemInstance in result.GrantedItems)
            {
                Debug.Log(itemInstance.ItemId + "-" + itemInstance.DisplayName + ": " + itemInstance.RemainingUses);
            }

            PlayFabInventory.instance.LazyLoadInventory();
        }

        protected virtual void RegisterError(PlayFabError error)
        {
            string textError = error.GenerateErrorReport();
            Debug.LogWarning(textError);
        }
    }
}