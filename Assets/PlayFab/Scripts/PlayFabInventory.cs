using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

namespace PlayFabDemo
{
    public class PlayFabInventory : MonoBehaviour
    {
        public static PlayFabInventory instance;

        private void Awake()
        {
            PlayFabInventory.instance = this;
        }


        public virtual void GetInventory()
        {

        }
    }
}