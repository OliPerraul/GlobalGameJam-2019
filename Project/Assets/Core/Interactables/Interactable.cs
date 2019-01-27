using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Reflection

namespace Core.Interactables
{
    public class Interactable : MonoBehaviour
    {

        public float interval = 30f;
        public float reduceAmount = 50f;
        public Image cooldownSlider;
        public SpriteRenderer ring;
        
        private float currentTime = 0;
        private Color ringColor;
        

        private void Start()
        {
            currentTime = 0;
            cooldownSlider.fillAmount = 0;
            ringColor = ring.color;
            ring.color = new Color(ringColor.r, ringColor.g, ringColor.b, 0);
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            cooldownSlider.fillAmount = currentTime / interval;
            if (cooldownSlider.fillAmount >= 1.0)
            {
                ring.color = new Color(ringColor.r, ringColor.g, ringColor.b, 1);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (currentTime > interval)
            {
                currentTime = 0;
                Interact();
            }
        }

        public void Interact()
        {
            Game.Instance.BuyStatus -= reduceAmount;
            
            Game.Instance.OnBuyStatusChangedHandler.Invoke();
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
