using UnityEngine;
using System.Collections;


namespace Core.Environment
{

    //UNUSED

    public class TriggerHelper : MonoBehaviour
    {
        public delegate void OnEnter(TriggerHelper trig, Collider other);
        public delegate void OnStay(TriggerHelper trig, Collider other);

        public enum IdEnum
        {
            One,
            Two
        }


        public OnEnter OnEnterHandler;

        public OnStay OnStayHandler;

        public void OnTriggerEnter(Collider other)
        {
            if(OnEnterHandler != null) OnEnterHandler.Invoke(this, other);

        }

        public void OnTriggerStay(Collider other)
        {
            if (OnStayHandler != null) OnStayHandler.Invoke(this, other);
        }

    }

}
