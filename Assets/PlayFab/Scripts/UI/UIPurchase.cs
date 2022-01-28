using TMPro;
using UnityEngine;

namespace PlayFabDemo
{
    public class UIPurchase : MonoBehaviour
    {
        public int price;

        public virtual void Purchase()
        {
            PlayFabPurchaseItem.instance.Purchase(transform.name, this.price);
        }
    }
}