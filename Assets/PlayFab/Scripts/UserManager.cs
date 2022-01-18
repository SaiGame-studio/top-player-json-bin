using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayFabDemo
{
    public class UserManager : MonoBehaviour
    {
        public GameObject userInput;

        public virtual void Login()
        {
            this.userInput.SetActive(false);
            GameManager.instance.GameStart();
        }
    }
}
