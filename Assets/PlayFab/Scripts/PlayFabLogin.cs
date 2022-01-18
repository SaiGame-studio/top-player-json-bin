using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

namespace PlayFabDemo
{
    public class PlayFabLogin : MonoBehaviour
    {
        public TMP_InputField username;
        public TMP_InputField password;

        private void Start()
        {
            this.username.text = "simonsai";
            this.password.text = "1234567";
        }

        public virtual void Login()
        {
            LoginWithPlayFabRequest loginRequest = new LoginWithPlayFabRequest
            {
                Username = this.username.text,
                Password = this.password.text
            };

            PlayFabClientAPI.LoginWithPlayFab(loginRequest, this.LoginSuccess, this.RegisterError);
        }

        protected virtual void LoginSuccess(LoginResult result)
        {
            Debug.Log("LoginSuccess");
            Debug.Log("PlayFabId:" + result.PlayFabId);
            Debug.Log("SessionTicket:" + result.SessionTicket);
            GameManager.instance.GameStart();
        }

        protected virtual void RegisterError(PlayFabError error)
        {
            string textError = error.GenerateErrorReport();
            Debug.LogWarning(textError);
        }
    }
}
