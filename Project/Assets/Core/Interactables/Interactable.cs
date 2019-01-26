using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Interactables
{
    public class Interactable : MonoBehaviour
    {
        public void Interact()
        {

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
