using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

namespace PlayFabDemo
{
    public class PlayFabInventory : MonoBehaviour
    {
        public static PlayFabInventory instance;
        public int seed = 0;
        public List<ItemInstance> invetory;

        private void Awake()
        {
            PlayFabInventory.instance = this;
        }

        public virtual void GetInventory()
        {
            GetUserInventoryRequest request = new GetUserInventoryRequest();
            PlayFabClientAPI.GetUserInventory(request, this.OnGetInventorySuccess, this.RequestError);
        }

        protected virtual void OnGetInventorySuccess(GetUserInventoryResult result)
        {
            this.seed = result.VirtualCurrency["SE"];
            UISeed.instance.Show(this.seed);

            this.invetory = result.Inventory;
            foreach(ItemInstance item in this.invetory)
            {
                UIInventory.instance.Show(item.ItemId, (int) item.RemainingUses);
            }
        }

        protected virtual void RequestError(PlayFabError error)
        {
            string textError = error.GenerateErrorReport();
            Debug.LogWarning(textError);
        }

        public virtual void AddSeed(int addCount)
        {
            AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest
            {
                VirtualCurrency = "SE",
                Amount = addCount
            };
            PlayFabClientAPI.AddUserVirtualCurrency(request, this.OnAddSeedSuccess, this.RequestError);
        }

        protected virtual void OnAddSeedSuccess(ModifyUserVirtualCurrencyResult result)
        {
            this.seed = result.Balance;
            UISeed.instance.Show(this.seed);
        }
    }
}