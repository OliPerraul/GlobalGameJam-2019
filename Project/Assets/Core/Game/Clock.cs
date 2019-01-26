using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core
{
    public class Clock : MonoBehaviour
    {
        public static Clock Instance = null; //TODO remove

        public delegate void OnTick();
        public OnTick OnTickEvent;

        private void Awake()
        {
            Instance = this;
        }

        public void Update()
        {
            if (OnTickEvent != null)
            OnTickEvent.Invoke();
        }

    }
}
