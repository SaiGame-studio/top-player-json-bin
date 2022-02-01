using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace PlayFabDemo
{
    public class PlayFabCustomeEvent : MonoBehaviour
    {
        public static PlayFabCustomeEvent instance;

        private void Awake()
        {
            if (PlayFabCustomeEvent.instance != null) Debug.LogError("PlayFabCustomeEvent Error");
            PlayFabCustomeEvent.instance = this;
        }

        public virtual void Eat(string itemId)
        {

            WriteClientPlayerEventRequest request = new WriteClientPlayerEventRequest
            {
                EventName = "eat_"+itemId,
            };

            PlayFabClientAPI.WritePlayerEvent(request, this.WritePlayerEventSuccess, this.RegisterError);
        }

        protected virtual void WritePlayerEventSuccess(WriteEventResponse result)
        {
            Debug.Log("EventId: " + result.EventId);
            PlayFabInventory.instance.LazyLoadInventory();
            PlayFabLeaderBoard.instance.LazyLoadTopEater();
        }

        protected virtual void RegisterError(PlayFabError error)
        {
            string textError = error.GenerateErrorReport();
            Debug.LogWarning(textError);
        }
    }
}