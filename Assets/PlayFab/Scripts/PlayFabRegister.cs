using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

namespace PlayFabDemo
{
    public class PlayFabRegister : MonoBehaviour
    {
        public TMP_InputField email;
        public TMP_InputField username;
        public TMP_InputField password;

        private void Start()
        {
            //this.email.text = "tranminhduc18116@gmail.com";
            //this.username.text = "simonsai";
            //this.password.text = "1234567";
        }

        public virtual void SignUp()
        {
            RegisterPlayFabUserRequest registerRequest = new RegisterPlayFabUserRequest
            {
                Email = this.email.text,
                Password = this.password.text,
                Username = this.username.text,
                DisplayName = this.username.text,
            };

            PlayFabClientAPI.RegisterPlayFabUser(registerRequest, this.RegisterSuccess, this.RegisterError);
        }

        protected virtual void RegisterSuccess(RegisterPlayFabUserResult result)
        {
            Debug.Log("RegisterSuccess");
        }

        protected virtual void RegisterError(PlayFabError error)
        {
            string textError = error.GenerateErrorReport();
            Debug.LogWarning(textError);
        }
    }
}