using UnityEngine;

namespace PlayFabDemo
{
    public class UIEat : MonoBehaviour
    {
        public virtual void Eat()
        {
            string appleName = this.transform.name.Replace("_eat", "");
            Debug.Log("Eat: " + appleName);
            
            if (appleName == "seed_golden") PlayFabConsumeItem.instance.Consume(appleName);
            else PlayFabUnlockContainerItem.instance.Unlock(appleName);

            PlayFabCustomeEvent.instance.Eat(appleName);
        }
    }
}