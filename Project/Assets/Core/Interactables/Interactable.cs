using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Reflection

namespace Core.Interactables
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        private Resource Resource;


        public void Interact()
        {
            Game.Instance.Values.BuyStatusComplete += Resource.Added.BuyStatusComplete;
            Game.Instance.Values.BuyStatusIncrement += Resource.Added.BuyStatusIncrement;
            Game.Instance.Values.HousePrice += Resource.Added.HousePrice;
            Game.Instance.Values.MaxHP += Resource.Added.MaxHP;
            Game.Instance.Values.MinHP += Resource.Added.MinHP;
            //Game.Instance.Values.NumVisitorMax += Resource.Added.BuyStatusIncrement;
            Game.Instance.Values.StartBuyStatus += Resource.Added.StartBuyStatus;
            Game.Instance.Values.TrapSpawnFreqMax += Resource.Added.TrapSpawnFreqMax;
            Game.Instance.Values.TrapSpawnFreqMin += Resource.Added.TrapSpawnFreqMin;
            Game.Instance.Values.VisitTimeMax += Resource.Added.VisitTimeMax;
            Game.Instance.Values.VisitTimeMin += Resource.Added.VisitTimeMin;


        }

        public void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Player>() != null)
            {
                other.GetComponent<Player>().Interactable = this;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Player>() != null)
            {
                other.GetComponent<Player>().Interactable = null;
            }
        }

    }
}
